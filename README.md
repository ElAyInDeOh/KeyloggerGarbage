# 🛡️ KeyloggerGarbage

**KeyloggerGarbage** is a lightweight, experimental utility that helps obscure real keyboard input from basic keyloggers by injecting fake keystrokes into the input stream. It is *not* a full security solution — just an obfuscation tool for specific situations.

![KeyloggerGarbageScreenshot](https://github.com/user-attachments/assets/4f322496-2ddc-421f-903b-0f0836e99381)


---

## 🔍 Overview

KeyloggerGarbage attempts to confuse basic keylogging tools through:

- **Garbage Keystroke Injection** – Random fake keystrokes that show up in keyloggers but don’t affect actual input  
- **Shadow Typing** – Characters that resemble real input are occasionally injected  
- **Special Key Injection** – Sends random arrow keys, function keys, etc.  
- **Variable Timing** – Random delays prevent predictable input patterns  

---

## 🛡️ Threats It Helps Obfuscate

These are common types of keyloggers and scenarios where KeyloggerGarbage can help:

✅ **Effectively Obfuscated:**

- Global hook-based keyloggers (`SetWindowsHookEx`)
- Polling-based keyloggers (`GetAsyncKeyState`, `GetKeyState`, etc.)
- Basic USB/PS2 hardware keyloggers (without timing analysis)
- Amateur keyloggers from GitHub, Pastebin, forums
- Phishing payloads in game cracks, cheat tools, or keygens
- Freeware spy tools disguised as “monitoring” software
- Parental control or snooping apps
- Basic employee surveillance tools
- Lightweight RATs using basic logging methods
- Commodity malware with crude keyloggers

⚠️ **Limited or No Effectiveness:**

- Advanced malware with timing analysis or multi-method capture
- Screen capture malware
- Form grabbers
- Memory-reading tools
- Sophisticated keyloggers using AI-based filtering or low-level drivers

---

## ⚙️ System Requirements

- Windows 7, 8, 10, or 11  
- .NET Framework 4.7.1 or higher  
- Administrator privileges (for keyboard hook access)  

---

## 🚀 Getting Started

1. Download the latest release from the [Releases](https://github.com/yourusername/KeyloggerGarbage/releases) page  
2. Extract all files  
3. Right-click `KeyloggerGarbage.exe` → **Run as Administrator**

---

## 💡 Usage

- Click **"Enable Protection"** to begin injecting obfuscation
- Use the **protection level slider** to adjust injection frequency  
  - **Low =** Better performance, minimal protection  
  - **High =** More protection, potentially noticeable lag
- View real and fake input in the **Live Input** panel
- Use the **Activity Log** to review injection events  
- Click **Export Log** to save data for manual review

---

## 🧪 Effectiveness Summary

| Threat Type                     | Protection |
|--------------------------------|------------|
| Hook-based keyloggers          | ✅ Effective |
| Polling-based keyloggers       | ✅ Effective |
| Hardware loggers (no timing)   | ✅ Effective |
| Advanced malware, AI detection | ❌ Ineffective |
| Screen capture, form grabbers  | ❌ Ineffective |

---

## ⚠️ Limitations

- **Does not detect keyloggers**
- **Does not prevent malware**
- **Cannot protect against screen capture or memory scraping**
- Some software (especially games) may not behave well with protection enabled
- Higher protection settings may cause slight performance impact

> 🧠 Use this tool *only* when entering sensitive data, and always alongside trusted security software

---

## 🛠️ Troubleshooting

| Problem                        | Solution                                       |
|-------------------------------|------------------------------------------------|
| App won’t launch              | Run as Administrator, check .NET version      |
| Typing feels weird/buggy      | Lower the protection level                    |
| App causes slowdown           | Disable protection when not entering sensitive info |

---

## 📜 License

MIT License – use freely. No data is collected, stored, or transmitted.  
All logs remain entirely local.

---
If you'd like to support my work, feel free to donate Bitcoin:
---
Bitcoin Address:
---

bc1qxu67tqhzzwfn6dyr7z2gq5d4wdexz7sa7d2pxe
---
Bitcoin QR Code
---

![BitcoinQRCode](https://github.com/user-attachments/assets/18df8d50-ab93-4922-ad47-5a9a1b890793)


*Need more help? Reach out via GitHub Issues for support.*
