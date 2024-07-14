using FluentResults;
using Newtonsoft.Json;

namespace Xm.TestTask.Services.Utilities;

public static class SerializerUtilities
{
    public static Result<T> DeserializeFromBytes<T>(byte[] data)
    {
        var dataString = System.Text.Encoding.UTF8.GetString(data);
        return Deserialize<T>(dataString);
    }

    public static Result<T> Deserialize<T>(string data)
    {
        try
        {
            var result = JsonConvert.DeserializeObject<T>(data);

            if (result is null)
                return Result.Fail<T>("Deserialized object is null");

            return Result.Ok(result);
        }
        catch (Exception e)
        {
            return Result.Fail<T>("Faild to deserialize").WithError(e.Message);
        }
    }
}
