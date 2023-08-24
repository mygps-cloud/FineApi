namespace FineApi.Service.Exception;

public class NoUserCarInformationException:System.Exception
{
    public NoUserCarInformationException():base("There Are No Cars") { }
}