class ICache{
  bool Set(string key, string val)
  {}
  string Get(string key)
  {}
}

class Redis : ICache
{
  public bool Set(string key, string val)
  {}
  public string Get(string key)
  {}
}

class ExchangeServiceWithDependencyInjectedByConstructor
{
  private ICache _cache;

  public ExchangeService(ICache cache){
    _chache = cache;
  }
  public double Convert(double Value, string CurrentCurrency,
    string TargetCurrency)
  {
    var rate = cache.Get(CurrentCurrency + "-" + TargetCurrency);
    if(rate){
      return Value * (double)rate;
    }else{
      var rateFromInternet = Http.....
      cache.Set(CurrentCurrency + "-" + TargetCurrency, rateFromInternet);
      return Value * rate;
    }
  }
}

class ExchangeServiceWithDependencyInjectedByParameter
{
  public double Convert(ICache cache, double Value, string CurrentCurrency,
    string TargetCurrency)
  {
    var rate = cache.Get(CurrentCurrency + "-" + TargetCurrency);
    if(rate){
      return Value * (double)rate;
    }else{
      var rateFromInternet = 190; // hacer llamado http al open exchange rates
      cache.Set(CurrentCurrency + "-" + TargetCurrency, rateFromInternet);
      return Value * rate;
    }
  }
}


var exchange = new ExchangeServiceWithDependencyInjectedByConstructor(new Redis());
var y = exchange.Convert(319, "COP", "USD")

var exchange2 = new ExchangeServiceWithDependencyInjectedByParameter();
var myCache = new Redis();
var z = exchange2.Convert(myCache, 319, "COP", "USD")

