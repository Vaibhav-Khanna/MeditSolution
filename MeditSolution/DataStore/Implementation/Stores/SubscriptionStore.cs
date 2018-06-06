using System;
using System.Linq;
using System.Threading.Tasks;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using MeditSolution.Models.DataObjects;
using FreshMvvm;
using MeditSolution.DataStore.Abstraction.Stores;
using MeditSolution.DataStore.Abstraction;
using MeditSolution.Helpers;

namespace MeditSolution.DataStore.Implementation.Stores
{
    public class SubscriptionStore
    {

        private IInAppBilling billing = CrossInAppBilling.Current;

        public string MonthlySubscriptionID { get; } = "abo.meditsolution.month";
        public string AnnuallySubscriptionID { get; } = "abo.meditsolution.an";

        private User user;
		private IStoreManager StoreManager = FreshIOC.Container.Resolve<IStoreManager>();
        

        public SubscriptionStore()
        {    
        }
        
        public async Task<List<InAppBillingProduct>> GetSubscriptionInfo()
        {
            try
            {
                var productIds = new string[] { MonthlySubscriptionID, AnnuallySubscriptionID };
                //You must connect
				var connected = await billing.ConnectAsync(ItemType.Subscription);

                if (!connected)
                {
                    //Couldn't connect
                    return null;
                }

                //check purchases

                var items = await billing.GetProductInfoAsync(ItemType.Subscription, productIds);

                return items.ToList();

            }
            catch (InAppBillingPurchaseException ex)
            {
                Debug.WriteLine(ex.Message);
                //Handle IAP Billing Exception
            }
            catch (Exception)
            {
                //Something has gone wrong
            }
            finally
            {
                await billing.DisconnectAsync();
            }

            return null;

        }



        public async Task<Tuple<bool, InAppBillingPurchase>> CheckAndUpdateSubscriptionStatus()
        {
            try
            {

                if (user == null)
                {
					user = await StoreManager.UserStore.GetCurrentUser();

                    if (user == null)
                        return null;
                }

                if( user.IsExplicitPremium )
                {
                    if (DateTime.Now.Date.CompareTo(user.ExplicitSubscriptionInfo.ExpirationDate.Date) <= 0)
                    {
                        await UpdateUserSubStatus(SubscriptionType.premium, "ExplicitPremium");

                        return new Tuple<bool, InAppBillingPurchase>(true, new InAppBillingPurchase() { ProductId = user.ExplicitSubscriptionInfo.ProductID, TransactionDateUtc = user.ExplicitSubscriptionInfo.ActivatedDate });
                    }                      
                }


				var connected = await billing.ConnectAsync(ItemType.Subscription);

                if (!connected)
                {
                    //Couldn't connect
                    return null;
                }

                //check purchases
                var purchases = await billing.GetPurchasesAsync(ItemType.Subscription);

                //check for null just incase
                if (purchases != null && purchases.Count() != 0)
                {
                    InAppBillingPurchase item;

                    if (Device.RuntimePlatform == Device.Android)
                    {
                        item = purchases.Where((arg) => arg.AutoRenewing).Count() != 0 ? purchases.Where((arg) => arg.AutoRenewing).FirstOrDefault() : purchases.FirstOrDefault();
                    }
                    else
                    {
                        item = purchases.LastOrDefault();
                    }

                    if (item.ProductId == AnnuallySubscriptionID || item.ProductId == MonthlySubscriptionID)
                    {

                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            if (item.State == PurchaseState.Purchased || item.State == PurchaseState.Restored)
                            {
                                await UpdateUserSubStatus(SubscriptionType.premium, item.Id);

                                return new Tuple<bool, InAppBillingPurchase>(true, item);
                            }
                            else
                            {
                                await UpdateUserSubStatus(SubscriptionType.free, null);
                                return null;
                            }
                        }
                        else if (Device.RuntimePlatform == Device.Android)
                        {
#if UAT
                            if (item.State == PurchaseState.FreeTrial || item.State == PurchaseState.PaymentPending || item.State == PurchaseState.Purchased)
                            {
                                if (item.AutoRenewing)
                                {
                                    await UpdateUserSubStatus(SubscriptionType.premium, item.PurchaseToken);
                                    return new Tuple<bool, InAppBillingPurchase>(true, item);
                                }
                                else
                                {
                                    await UpdateUserSubStatus(SubscriptionType.free, null);
                                    return null;
                                }
                            }
                            else
                            {
                                await UpdateUserSubStatus(SubscriptionType.free, null);
                                return null;
                            }
#else
                            if (item.State == PurchaseState.Purchased || item.State == PurchaseState.PaymentPending || item.State == PurchaseState.FreeTrial)
                            {
                                if (item.AutoRenewing)
                                {
                                    await UpdateUserSubStatus(SubscriptionType.premium, item.PurchaseToken);
                                    return new Tuple<bool, InAppBillingPurchase>(true, item);
                                }
                                else
                                {
                                    await UpdateUserSubStatus(SubscriptionType.free, null);
                                    return null;
                                }
                            }
                            else
                            {
                                await UpdateUserSubStatus(SubscriptionType.free, null);
                                return null;
                            }
#endif

                        }

                    }

                    await UpdateUserSubStatus(SubscriptionType.free, null);
                    return null;
                    //Purchase restored      
                }
                else
                {
                    //no purchases found
                    await UpdateUserSubStatus(SubscriptionType.free, null);
                    return null;
                }

            }
            catch (InAppBillingPurchaseException purchaseEx)
            {
                //Billing Exception handle this based on the type
                await UpdateUserSubStatus(SubscriptionType.free, null);

                Debug.WriteLine("Error: " + purchaseEx.Message);

            }
            catch (Exception)
            {
                //Something has gone wrong
            }
            finally
            {
                await billing.DisconnectAsync();
            }

            return null;
        }



        public async Task<bool> PurchaseSubscription(string productId)
        {

            try
            {
				var connected = await billing.ConnectAsync(ItemType.Subscription);

                if (!connected)
                {
                    //we are offline or can't connect, don't try to purchase
                    return false;
                }

                //check purchases
                var purchase = await billing.PurchaseAsync(productId, ItemType.Subscription, productId);

                //possibility that a null came through.
                if (purchase == null)
                {
                    return false;
                    //did not purchase
                }
                else
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        if (purchase.State == PurchaseState.Purchased || purchase.State == PurchaseState.Restored)
                        {
                            await UpdateUserSubStatus(SubscriptionType.premium, purchase.Id);
                            return true;
                        }
                        else
                            return false;
                    }
                    else if (Device.RuntimePlatform == Device.Android)
                    {

#if UAT
                        if (purchase.State == PurchaseState.Purchased || purchase.State == PurchaseState.FreeTrial || purchase.State == PurchaseState.PaymentPending)
                        {
                            if (purchase.AutoRenewing)
                            {
                                await UpdateUserSubStatus(SubscriptionType.premium, purchase.PurchaseToken);
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
#else

                        if (purchase.State == PurchaseState.Purchased || purchase.State == PurchaseState.FreeTrial || purchase.State == PurchaseState.PaymentPending )
                        {
                            if (purchase.AutoRenewing)
                            {
                        await UpdateUserSubStatus(SubscriptionType.premium, purchase.PurchaseToken);
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;

#endif

                    }

                    //purchased!
                }
            }
            catch (InAppBillingPurchaseException purchaseEx)
            {
                //Billing Exception handle this based on the type
                Debug.WriteLine("Error: " + purchaseEx);
            }
            catch (Exception ex)
            {
                //Something else has gone wrong, log it
                Debug.WriteLine("Issue connecting: " + ex);
            }
            finally
            {
                await billing.DisconnectAsync();
            }

            return false;
        }
        

        async Task UpdateUserSubStatus(SubscriptionType type, string orderID)
        {
            if (user == null)
            {
				user = await StoreManager.UserStore.UpdateCurrentUser(null);

                if (user == null)
                    return;
            }

            if (user.Subscription != type)
            {
                if (type == SubscriptionType.free)
                {
					user = await StoreManager.UnSubscribeToPremium("me");

					Settings.User = Newtonsoft.Json.JsonConvert.SerializeObject(user);

                    MessagingCenter.Send(this, "SubscriptionChanged");
                }
                else
                {
                    user.Subscription = SubscriptionType.premium;

                    var _premium = new SubscriptionPremium
                    {
                        Ticket = orderID,
                        Platform = Device.RuntimePlatform.ToLower()
                    };

					user = await StoreManager.SubscribeToPremium("me", _premium);

					Settings.User = Newtonsoft.Json.JsonConvert.SerializeObject(user);

                    MessagingCenter.Send(this, "SubscriptionChanged");
                }
            }
        }

        public void Dispose()
        {
            CrossInAppBilling.Dispose();
        }

    }
}
