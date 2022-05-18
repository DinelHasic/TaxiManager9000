// See https://aka.ms/new-console-template for more information
using Hellper;
using TaxiManager9000.Domain.Enums;
using TaxiManager9000.Domain.Exceptions;
using TaxiManager9000.Services;
using TaxiManager9000.Services.Interfaces;
using TaxiManager9000.Services.Services;
using TaxiManager9000.UI.Utils;

IAuthService authService = new AuthService();
IAdminServices adminServices = new AdminServices();
IMaintenanceServices maintenanceServices = new MaintenanceServices();
IManagerServices managerServices = new ManagerService();

StartApplication(authService);

void StartApplication(IAuthService authService)
{
    ShowLogin(authService);
    ShowMenuAsync(authService);
}

void ShowLogin(IAuthService authService)
{
    Console.WriteLine("Enter username");
    string username = Console.ReadLine();

    Console.WriteLine("Enter password");
    string password = Console.ReadLine();

    try
    {
        authService.LogIn(username, password);

        ConsoleUtils.WriteLineInColor($"Successful login! Welcome {authService.CurrentUser.Role} {authService.CurrentUser.UserName}", ConsoleColor.Green);
    }
    catch (ArgumentNullException e)
    {
        ConsoleUtils.WriteLineInColor($"Unsuccessful login, try again", ConsoleColor.Red);
        ShowLogin(authService);
    }
}

async Task ShowMenuAsync(IAuthService authService)
{
    switch (authService.CurrentUser.Role)
    {
        case Role.Administrator:
            await ShowAdminMenuAsync(authService);
            break;
        case Role.Maintainance:
            await ShowMaintainenceMenu(authService);
            break;
        case Role.Manager:
            await ShowManagerMenuAsync(authService);
            break;
        default:
            ConsoleUtils.WriteLineInColor($"Invalid role, {authService.CurrentUser.Role}", ConsoleColor.Red);
            break;
    }
}

///------------ADMIN-----------------------///
async Task ShowAdminMenuAsync(IAuthService authService)
{
    Console.WriteLine("Enter number for options:\n1)AddUser \n2)RemoveUser\n3)CreatePassword\n4)MainManue\n5)Exit");
    string input = Console.ReadLine().ToUpper();

    switch (input)
    {
        case AdminOptionManue.ADD_USER:
            await CreateUserAsync();
            break;
        case AdminOptionManue.REMOVE_USER:
            await RemoveUser();
            break;
        case AdminOptionManue.NEW_PASSWORD:
            await CreateNewPassword();
            break;
        case AdminOptionManue.MAIN_MANUE:
            StartApplication(authService);
            break;
        case AdminOptionManue.EXIT_USER:
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Ivalid input");
            break;
    }
}
async Task CreateUserAsync() /// Admin option to Create a User
{
    Console.WriteLine("Enter users username:");
    string username = Console.ReadLine();

    Console.WriteLine("Enter users password");
    string password = Console.ReadLine();

    Console.WriteLine("Enter role name:\n1)Administrartor\n2)Manager\n3)Maintainance:");
    string role = Console.ReadLine();

    try
    {
        await adminServices.CreateUserAsync(username, password, role.ConvertRole());
        ConsoleUtils.WriteLineInColor($"Successful creation of  user!", ConsoleColor.Green);
        ShowMenuAsync(authService);
    }
    catch (InvalidUserCreatinoExpection e)
    {
        ConsoleUtils.WriteLineInColor($"{e.Message} Creation unsuccessful. Please try again.", ConsoleColor.Red);
        CreateUserAsync();
    }
}

async Task RemoveUser() /// Admin option to Remove a User
{
    Console.WriteLine("Choose a username that you want to remove user:");

    List<string> usernames = adminServices.ListOfUserNames();
    usernames.ForEach(Console.WriteLine);

    Console.WriteLine("Enter username to remove user:");
    string usersUsername = Console.ReadLine();

    try
    {
        await adminServices.RemoveUserAsync(usersUsername);
        ConsoleUtils.WriteLineInColor("User is successful removed", ConsoleColor.Green);
    }
    catch (InvalidUserRemoveExpextion)
    {
        ConsoleUtils.WriteLineInColor("User unsccessful removed", ConsoleColor.Red);
    }

    ShowAdminMenuAsync(authService);
}

/// -------------- Maintainance -------------------///
async Task ShowMaintainenceMenu(IAuthService authService)
{
    Console.WriteLine("Enter number for options:\n1)List all vehicles\n2)License Plate Status\n3)ChangePassword\n4)MainManue\n5)Exit");

    string input = Console.ReadLine().ToUpper();
    Console.Clear();

    switch (input)
    {
        case MaintainenceOptionManue.LIST_OF_VHICHELES:
            PrintListOfCars();
            break;
        case MaintainenceOptionManue.LICENSE_PLAE_STATUS:
            LicensePlateStatus();
            break;
        case MaintainenceOptionManue.CHANGE_PASSWORD:
            await CreateNewPassword();
            break;
        case MaintainenceOptionManue.MAIN_MANUE:
            StartApplication(authService);
            break;
        case MaintainenceOptionManue.EXIT_USER:
            Environment.Exit(0);
            break;
        default:
            throw new NotImplementedException("Invalid  input");
    }
    Console.WriteLine("Press enter to continue......");
    Console.ReadLine();
    ShowMaintainenceMenu(authService);
}

void PrintListOfCars()
{
    maintenanceServices.GetListOfCars().ForEach(x => ConsoleUtils.WriteLineInColor(x.ToString(),ConsoleColor.DarkCyan));
}

void LicensePlateStatus()
{
    maintenanceServices.GetValidLicensePlate().ForEach(x => ConsoleUtils.WriteLineInColor($"{x.Id} - Plate {x.LicensePlate} expiering on [{x.LicensePlateExpieryDate}]", ConsoleColor.Green));

    maintenanceServices.GetLicensePlateToExpired().ForEach(x => ConsoleUtils.WriteLineInColor($"{x.Id} - Plate {x.LicensePlate} expiering on [{x.LicensePlateExpieryDate}]", ConsoleColor.Yellow));

    maintenanceServices.GetExpiredLicensePlate().ForEach(x => ConsoleUtils.WriteLineInColor($"{x.Id} - Plate {x.LicensePlate} expiering on [{x.LicensePlateExpieryDate}]", ConsoleColor.Red));
}

/// -------------Manager----------------///
async Task ShowManagerMenuAsync(IAuthService authService)
{
    Console.WriteLine("Enter a number for options:\n1)List all drivers\n2)Taxi License Status\n3)Driver Manager\n4)Change Password\n5)MainManue\n6)Exit");
    string option = Console.ReadLine();
    Console.Clear();

    switch (option)
    {
        case MangerOptionManue.LIST_ALL_DRIVERS:
            managerServices.ListOfAllDrivers().ForEach(x => ConsoleUtils.WriteLineInColor(x.ToString(),ConsoleColor.Cyan));
            break;
        case MangerOptionManue.TAXI_LICENSE_STATUS:
            LicenseStatus();
            break;
        case MangerOptionManue.DRIVER_MANAGER:
            await DriverMnagerAsync();
            break;
        case MangerOptionManue.CHANGE_PASSWORD:
            await CreateNewPassword();
            break;
        case MangerOptionManue.MAIN_MANUE:
            StartApplication(authService);
            break;
        case MangerOptionManue.EXIT_USER:
            Environment.Exit(0);
            break;
        default:
            throw new Exception("Incorect implementation");
    }
    Console.WriteLine("Press enter to continue......");
    Console.ReadLine();
    ShowManagerMenuAsync(authService);
}

//===== LICENSE STATUS ====//
void LicenseStatus()
{
    managerServices.GetValidLicense().ForEach(x => ConsoleUtils.WriteLineInColor($"FullName:{x.FirstName} {x.LastName} with license {x.License} expering on {x.LicenseExpieryDate}", ConsoleColor.Green));

    managerServices.GetLicenseToExpired().ForEach(x => ConsoleUtils.WriteLineInColor($"FullName:{x.FirstName} {x.LastName} with license {x.License} expering on {x.LicenseExpieryDate}", ConsoleColor.Yellow));

    managerServices.GetExpiredLicense().ForEach(x => ConsoleUtils.WriteLineInColor($"FullName:{x.FirstName} {x.LastName} with license {x.License} expering on {x.LicenseExpieryDate}", ConsoleColor.Red));
}

// ==== Driver Manager ==//
async Task DriverMnagerAsync()
{
    Console.WriteLine("Enter a number for options:\n1)Assign Unassigned Drivers\n2)Unasign Assigned Drivers");
    string input = Console.ReadLine();
    switch (input)
    {
        case "1":
            await AssignUnassignedDriversAsync();
            break;
        case "2":
            await UnasignAssignedDriversAsync();
            break;
        default:
            throw new Exception("Incorect implementation");
            break;
    }
}

// == ASSIGN CAR TO DRIVERS ===//
async Task AssignUnassignedDriversAsync()
{
    Console.WriteLine("List of all unasigned drivers:");
    managerServices.ListOfUnasignDrives().ForEach(x => ConsoleUtils.WriteLineInColor($"FullName:{x.FirstName} {x.LastName} Id:{x.Id}\n", ConsoleColor.Blue));

    Console.Write("Choose one of the drivers that are provided.\nEnter Id of the driver:");
    string driverId = Console.ReadLine();


    Console.WriteLine("Enter shift:\n1)Morning\n2)Afternoon\n3)Evening");
    string shift = Console.ReadLine();

    try
    {
        Console.WriteLine("List of avaliable car:");
        maintenanceServices.GetListAvailableCars(shift.ConvertShift()).ForEach(x => ConsoleUtils.WriteLineInColor($"Id:{x.Id} Model:{x.Model} LicensePlate:{x.LicensePlate}\n", ConsoleColor.Blue));
    }
    catch (Exception ex)
    {
        ConsoleUtils.WriteLineInColor($"{ex.Message}.Try again", ConsoleColor.Red);
        AssignUnassignedDriversAsync();
    }

    Console.Write("Choose one of the cars that are provided.\nEnter Id of car:");
    string carId = Console.ReadLine();

    try
    {
        await managerServices.AssignCarAndDriverAsync(shift.ConvertShift(),carId.ConverToNumber(), driverId.ConverToNumber());

        ConsoleUtils.WriteLineInColor($"Assigned Sucessful", ConsoleColor.Green);
    }
    catch (Exception e)
    {
        ConsoleUtils.WriteLineInColor($"{e.Message}.Try again", ConsoleColor.Red);
        AssignUnassignedDriversAsync();
    }

    ShowManagerMenuAsync(authService);
}

// == UNASIGN DRIVER ==// 
async Task UnasignAssignedDriversAsync()
{
    Console.WriteLine("List of all assigned drivers");
    managerServices.ListOfAssignedDrives().ForEach(x => ConsoleUtils.WriteLineInColor($"FullName:{x.FirstName} {x.LastName} Id:{x.Id}\n", ConsoleColor.Cyan));

    Console.Write("Choose one of the drivers that are provided.\nEnter Id of the driver:");
    string id = Console.ReadLine();

    try
    {
        await managerServices.UnasignCarToDriverAsync(id.ConverToNumber());

        ConsoleUtils.WriteLineInColor($"Assigned Sucessful", ConsoleColor.Green);
    }
    catch (Exception e)
    {
        ConsoleUtils.WriteLineInColor($"{e.Message}.Try again", ConsoleColor.Red);
        UnasignAssignedDriversAsync();
    }

    ShowManagerMenuAsync(authService);
}
// === Create Password ===//
async Task CreateNewPassword()
{
    Console.WriteLine("Enter old password");
    string oldpassword = Console.ReadLine();

    Console.WriteLine("Enter new password");
    string newpassword = Console.ReadLine();

    try
    {
        await authService.SetNewPasswordAsync(oldpassword, newpassword);
        ConsoleUtils.WriteLineInColor("Successful change password!", ConsoleColor.Green);
    }
    catch (InvalidPasswordCreationExpection e)
    {
        ConsoleUtils.WriteLineInColor($"{e.Message}. Please try again", ConsoleColor.Red);
        CreateNewPassword();
    }

    StartApplication(authService);
}

