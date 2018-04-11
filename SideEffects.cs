using System.Collections.Generic;

class Redis : ICache
{
  Dictionary<string, string> _store;

  public Redis()
  {
    _store = new Dictionary<string, string>();
  }

  public void Set(string key, string val)
  {
    if(!_store.ContainsKey(key)){
      _store.Add(key, val);
    }
  }

  public string Get(string key)
  {
    if(_store.ContainsKey(key)){
      return _store[key];
    }
    return null;
  }
}

public interface IHttp 
{
  string Get(string url);
}

class Http : IHttp
{
  public string Get(string url)
  {
    throw new System.InvalidOperationException("Timeout Daniela");
    return "1";
  }
}

public interface ICache
{
  void Set(string key, string val); 
  string Get(string key);
}

public interface ILogger
{
  void Write(string message);
}

class StandardIO : ILogger
{
  public void Write(string message){
    System.Console.WriteLine(message);
  }
}
class ExchangeService
{
  ICache _cache;
  IHttp _http;
  ILogger _console;

  public ExchangeService(ICache cache, IHttp http, ILogger console){
    _cache = cache;
    _http = http;
   _console = console;
  }

  public double ConvertTo(double Value, string CurrentCurrency, string TargetCurrency)
  {
    var key = CurrentCurrency + "-" + TargetCurrency;
    var rate = _cache.Get(key);

    if(rate != null){
      _console.Write("Se encontro la tasa en el cache" + rate);
      return Value * double.Parse(rate);
    }else{
      _console.Write("No se encontro la tasa en el cache entonces la voy a buscar en internet");
      string rateFromInternet;
      try {
        rateFromInternet = _http.Get("http://www.openexchangerates.com/?currencies="+key+"&value="+Value);
        _cache.Set(rateFromInternet, rateFromInternet);
      }
      catch(System.Exception e){
        _console.Write(e.Message);
        rateFromInternet = "1";
      }
      return Value * double.Parse(rateFromInternet);
    }
  }
}


public class SideEffects
{
  static public void Main()
  {
    var exchange = new ExchangeService(new Redis(), new Http(), new StandardIO());

    var newValue = exchange.ConvertTo(13.5, "USD", "COP");
    System.Console.WriteLine(newValue);

    newValue = exchange.ConvertTo(5, "USD", "COP");
    System.Console.WriteLine(newValue);

    newValue = exchange.ConvertTo(13.5, "USD", "BRL");
    System.Console.WriteLine(newValue);
  }
}
