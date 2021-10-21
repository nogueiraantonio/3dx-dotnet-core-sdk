Usage of the elements in this library to authenticate:

    AuthenticationSelectionForm authSelectionForm = new AuthenticationSelectionForm();

    if (authSelectionForm.ShowDialog(this) != DialogResult.OK)
        return;

    IAuthenticationType authenticationType = authSelectionForm.SelectedAuthenticationType;

    if (authenticationType.ShowForm(this) != DialogResult.OK)
        return;

    AuthenticationInfo authInfo = authenticationType.GetAuthenticationInfo();

    if (!authInfo.IsValidAuthentication)
        return;
    
    IPassportAuthentication passport = authInfo.Passport;

    string securityContext = authInfo.SecurityContext;
    string enoviaURL       = authInfo.EnoviaURL;

    m_userLoginInfo.Text   = authInfo.LoginMessage;

    