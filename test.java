import java.io.IOException;
import java.net.InetAddress;
import java.net.UnknownHostException;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.Locale;
import java.util.Properties;


public static void main(String[] args){
        System.out.println("Hello, World!");

        System.out.println("====== SystemInfo ======");
        System.out.println("Internet Info: " + (isNetworkConnected() ? "Is connteced" : "Not connected"));

        getSystemInfo();
        getCurrentTime();
        getSystemLanguage();
}


private static boolean isNetworkConnected(){
    try{
        InetAddress address = InetAddress.getByName("www.bing.com");
        return address.isReachable(3000);
    }catch(UnknownHostException e){
        System.out.println("Network error: Cannot read hostname");
        return false;
    }catch(IOException e){
        System.out.println("Network error: " + e.getMessage());
        return false;
    }
}

private static void getSystemInfo(){
    Properties properties = System.getProperties();

    System.out.println("SYstem name: " + properties.getProperty("os.name"));
    System.out.println("System version: " + properties.getProperty("os.version"));
    System.out.println("System arch: " + properties.getProperty("os.arch"));
    System.out.println("Java version: " + properties.getProperty("java.version"));
    System.out.println("Java vender:" + properties.getProperty("java.vendor"));
    System.out.println("Java vm name: " + properties.getProperty("Java.vm.name"));
    System.out.println("User home: " + properties.getProperty("user.home"));
    System.out.println("User dir: " + properties.getProperty("user.dir"));
}

private static void getCurrentTime(){
    LocalDateTime now = LocalDateTime.now();
    DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss");

    System.out.println("Now Time: " + now.format((formatter)));
    System.out.println("Year :" + now.getYear());
    System.out.println("Month: " + now.getMonthValue() + "(" + now.getMonth() + ")");
    System.out.println("Date: " + now.getDayOfMonth());
    System.out.println("Week: " + now.getDayOfWeek());
    System.out.println("Hour: " + now.getHour());
    System.out.println("Minute: " + now.getMinute());
    System.out.println("Second: " + now.getSecond());
}

private static void getSystemLanguage(){
    Locale locale = Locale.getDefault();

    System.out.println("System Languages: " +locale.getDisplayLanguage());
    System.out.println("Languages: " + locale.getLanguage());
    System.out.println("Country: " + locale.getDisplayCountry());
    System.out.println("Country Code: " + locale.getCountry());
}

class Character {
    enum Girls{Akane, Ayano, Sakura}
    Girls girls;
    
}