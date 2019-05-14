using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data;
using BloodTrace.Services;

namespace BloodTrace.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            
            InitializeComponent();
            //Remove navigation bar from the Loigin Page
            NavigationPage.SetHasNavigationBar(this, false);
        }
        //Loig method using values provided from Mobile Login UI
        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                ApiServices apiServices = new ApiServices();
                bool response = await apiServices.LoginUser(EntEmail.Text, EntPassword.Text);
                if (!response)
                {
                    await DisplayAlert("Alert", "Something Went Wrong", "Cancel");
                }
                else
                {
                    Navigation.InsertPageBefore(new HomePage(), this);
                    await Navigation.PopAsync();
                }
            }
           
            catch (Exception)
            {

                await DisplayAlert("Sorry", "Cannot connect to the server", "Exit");
            }
        }

        private void TapSignUp_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }
    }
}