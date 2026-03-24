#Copyright 2026 Sunset Doll. All rights reserved.
import sys
import platform
import locale
import socket
import subprocess

def is_connected() -> bool:
    try:
        socket.create_connection(("9.9.9.9", 53), timeout = 3)
        socket.create_connection(("223.5.5.5", 53), timeout = 3)
        return True
    except (socket.timeout, OSError):
        return False
    
def get_network_type() -> str:
    if not is_connected():
        return "Not connected."
    
    os_type = platform.system()

    try:
        if os_type == "Windows":
            try:
                subprocess.check_output(
                    ["netsh", "wlan", "show", "interfaces"],
                    stderr = subprocess.DEVNULL,
                    text = True
                )
                return "WiFi."
            except:
                return "Ethernet."

        elif os_type == "Darwin":
            return "WiFi" if "Wi-Fi" in subprocess.check_output(
                ["networksetup", "-listallhardwareports"], text = True
            ) else "Ethernet."
        
        elif os_type == "Linux":
            return "WiFi" if subprocess.check_output(
                ["iw", "dev"], stderr = subprocess.DEVNULL, text = True
            ) else "Ethernet."
        
    except:
        return "Unknow."

    return "Unknow"


def get_system_language_region():
    default_locale = locale.getdefaultlocale()

    if default_locale:
        language_code, region_code = default_locale
    else:
        language_code = "Unknow"
        region_code = "Unknow"

    lang_map = {
        "zh": "Chinese",
        "en": "English",
        "ja": "Japanese"
    }

    region_map = {
        "CN": "China",
        "US": "United State",
        "JP": "Japan",
        "HK": "Chinese HongKong",
        "TW": "Chinese Taiwan"
    }

    language = lang_map.get(language_code,language_code)
    region = region_map.get(region_code,region_code)

    return language, region, language_code, region_code

def check_os():
    os_type = platform.system()

    if os_type == "Windows":
        return "System type: Windows"
    elif os_type == "Darwin":
        return "System type: macOS"
    elif os_type == "Linux":
        return "System type: Linux"
    else:
        return f"System: Unknow system type ({os_type})"



if __name__ == "__main__":
    lang, region, lang_code, region_code = get_system_language_region()
    print(f"System Language: {lang} ({lang_code})")
    print(f"System region: {region} ({region_code})")

    print("===== System Messages =====")

    os_name = platform.system()
    print(f"System: {os_name}")

    net_status = "Is connected." if is_connected else "Not connected"
    print(f"Network stateus: {net_status}")

    net_type = get_network_type()
    print(f"Connected stateus: {net_type}")
    print("=" * 40)
