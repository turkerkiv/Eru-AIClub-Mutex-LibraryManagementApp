using LibraryManagementApp;
using LibraryManagementApp.View;

DatabaseManager.LoadAllRepos();

UIHandler uIHandler = new UIHandler();
uIHandler.RunTheApp();

DatabaseManager.SaveAllRepos();