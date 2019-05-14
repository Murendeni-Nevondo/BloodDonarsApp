using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodTrace.Pages;
using BloodTrace.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodTrace.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUpPage : ContentPage
	{
		public SignUpPage ()
		{
			InitializeComponent ();
           
            NavigationPage.SetHasNavigationBar(this, false);
		}

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            try
            {
                ApiServices apiServices = new ApiServices();
                var response = await apiServices.Register(EntEmail.Text, EntPassword.Text, EntConfirmPassword.Text);
                if (!response)
                {
                    await DisplayAlert("Hello", "Something went wrong", "Cancel");
                }
                else
                {
                    await DisplayAlert("Hello", "Your account has been created", "Ok");
                    await Navigation.PopToRootAsync();
                }
            }
            catch (Exception)
            {

                await DisplayAlert("Sorry", "Cannot connect to the server", "Exit");
            }
        }

        private void EntConfirmPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(EntPassword.Text != EntConfirmPassword.Text)
            {
                lblConfrimPassword.Text = "Password do not match";
                lblConfrimPassword.TextColor = Color.Red;
               
            }
            else
            {
                lblConfrimPassword.Text = "Password Matches";
                lblConfrimPassword.TextColor = Color.Green;
            }
        }
    }
}