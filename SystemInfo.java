import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.Locale;
import java.util.Properties;

public class SystemInfo {
    public static void main(String[] args){
        System.out.println("System: " + System.getProperty("os.name"));
        System.out.println("Time: " + LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss")));
        System.out.println("Languages: " + Locale.getDefault().getDisplayCountry());
        System.out.println("Country: " + Locale.getDefault().getDisplayCountry());
    } 
}
