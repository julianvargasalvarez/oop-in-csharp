using System.Collections.Generic;

public interface ISocialNetworkAccount {
  string ScreenName { get; set; }
  string Uid {get; set;}
  string FullName {get; set; }
  string Provider {get; }
  int Followers { get; set; }
  string Category {get; }
}


public abstract class SocialNetworkAccount : ISocialNetworkAccount, IFollowable
{
  public string ScreenName { get; set; }
  public string Uid {get; set;}
  public string FullName {get; set; }
  public string Provider {get; protected set; }
  public abstract int Followers { get; set; }
  public string Category
  {
    get {
      if(Followers <= 10){
        return "Citizen";
      }
      if(Followers <= 50){
        return "Professional";
      }
      return "Celebrity";
    }
  }
}


public class FacebookAccount : SocialNetworkAccount
{
  private int _friends;

  public FacebookAccount()
  {
    _friends = 0;
    this.Provider = "facebook";
  }

  public override int Followers
  {
    get {
      return _friends;
    }
    set {
      _friends = value;
    }
  }
}

public class FanpageAccount: SocialNetworkAccount
{
  private int _likers;

  public FanpageAccount()
  {
    _likers = 0;
    this.Provider = "fanpage";
  }

  public override int Followers
  {
    get {
      return _likers;
    }
    set {
      _likers = value;
    }
  }
}


public class YoutubeAccount: SocialNetworkAccount
{
  private int _subscribers;
  public YoutubeAccount()
  {
    _subscribers = 0;
    this.Provider = "youtube";
  }
  public override int Followers
  {
    get {
      return _subscribers;
    }
    set {
      _subscribers = value;
    }
  }
}

public class InstagramAccount: SocialNetworkAccount
{
  private int _followers;
  public InstagramAccount()
  {
    _followers = 0;
    this.Provider = "instagram";
  }
  public override int Followers
  {
    get {
      return _followers;
    }
    set {
      _followers = value;
    }
  }
}

public class TwitterAccount : ISocialNetworkAccount, IFollowable
{
  public string ScreenName { get; set; }
  public string Uid {get; set;}
  public string FullName {get; set; }
  public string Provider {get { return "twitter"; } }
  public int Followers { get; set; }
  public string Category
  {
    get {
      return "Celebrity";
    }
  }
}

public interface IFollowable
{
  int Followers { get; set;}
  string Provider { get; }
}

public class PriceCalculator
{
  Dictionary<string, ICalculable> _providers;

  public PriceCalculator()
  {
    _providers = new Dictionary<string, ICalculable>();
  }

  private Dictionary<string, ICalculable> Providers{ get { return _providers;} }


  public void AddProvider(string provider, ICalculable calculator){
    _providers.Add(provider, calculator);
  }

  public double CalculatePricePerPostFor(IFollowable influencer)
  {
    return _providers[influencer.Provider].CalculatePrice(influencer);
  }
}


public interface ICalculable{
  double CalculatePrice(IFollowable x);
}

public class FacebookPriceCalculator : ICalculable
{
  public double CalculatePrice(IFollowable influencer)
  {
        return influencer.Followers * 0.5;
  }
}

public class FanpagePriceCalculator : ICalculable
{
  public double CalculatePrice(IFollowable influencer)
  {
        return influencer.Followers * 0.1;
  }
}

public class YoutubePriceCalculator : ICalculable
{
  public double CalculatePrice(IFollowable influencer)
  {
        return influencer.Followers * 0.3;
  }
}

public class InstagramPriceCalculator : ICalculable
{
  public double CalculatePrice(IFollowable influencer)
  {
        return influencer.Followers * 0.09;
  }
}

public class TwitterPriceCalculator : ICalculable
{
  public double CalculatePrice(IFollowable influencer)
  {
        return influencer.Followers * 0.9;
  }
}


public class BinaryTreeIndexForData<T>
{
  public void Add(T item){
  }

  public T this[int index]{
    get {
      throw new System.NotImplementedException();
    }
  }
}

public class SocialNetworkAccountsApplication
{
  static public string ConvertToString(ISocialNetworkAccount sna)
  {
    return sna.Category + " " + sna.Followers + " " + sna.Provider + " " + sna.FullName +" "+ sna.Uid + " " + sna.ScreenName;
  }

  static public void Main()
  {
    FacebookAccount facebook = new FacebookAccount() { FullName="el Rey Midas", Uid="531902", ScreenName="Midas", Followers=10};
    FanpageAccount fanpage = new FanpageAccount() { FullName="Jose Alberto Camargo", Uid="910910", ScreenName="ElFanDelRey", Followers=50};
    YoutubeAccount youtube = new YoutubeAccount() { FullName="Jose Camargo", Uid="15319024", ScreenName="ElCanalDelRey", Followers=200};
    TwitterAccount twitter = new TwitterAccount() { FullName="Jose Camargo", Uid="15377024", ScreenName="app_config", Followers=300};
    InstagramAccount instagram = new InstagramAccount() { FullName="Jose Camargo", Uid="15377024", ScreenName="app_config", Followers=300};

/*
    System.Console.WriteLine(ConvertToString(facebook));
    System.Console.WriteLine(ConvertToString(fanpage));
    System.Console.WriteLine(ConvertToString(youtube));
    System.Console.WriteLine(ConvertToString(twitter));
    System.Console.WriteLine(ConvertToString(instagram));
    System.Console.WriteLine("=============================================================");
*/
    PriceCalculator priceCalculator = new PriceCalculator();
    priceCalculator.AddProvider("facebook", new FacebookPriceCalculator());
    priceCalculator.AddProvider("fanpage", new FanpagePriceCalculator());
    priceCalculator.AddProvider("youtube", new YoutubePriceCalculator());
    priceCalculator.AddProvider("twitter", new TwitterPriceCalculator());
    priceCalculator.AddProvider("instagram", new InstagramPriceCalculator());
/*
    System.Console.WriteLine(priceCalculator.CalculatePricePerPostFor(facebook));
    System.Console.WriteLine(priceCalculator.CalculatePricePerPostFor(fanpage));
    System.Console.WriteLine(priceCalculator.CalculatePricePerPostFor(youtube));
    System.Console.WriteLine(priceCalculator.CalculatePricePerPostFor(twitter));
    System.Console.WriteLine(priceCalculator.CalculatePricePerPostFor(instagram));
*/

  //  SocialNetworkAccount x = new FacebookAccount();
 //   System.Console.WriteLine(x);

    BinaryTreeIndexForData<IFollowable> myIndex = new BinaryTreeIndexForData<IFollowable>();
    myIndex.Add(facebook);
    myIndex.Add(fanpage);
    myIndex.Add(youtube);
    myIndex.Add(twitter);
    myIndex.Add(instagram);

    BinaryTreeIndexForData<string> myIndexForString = new BinaryTreeIndexForData<string>();
    myIndexForString.Add("hola mundo");

    BinaryTreeIndexForData<int> myIndexForInt = new BinaryTreeIndexForData<int>();
    myIndexForInt.Add(190999);
  }
}
