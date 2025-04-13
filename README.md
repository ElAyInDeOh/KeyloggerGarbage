🛡️ KeyloggerGarbage
KeyloggerGarbage is a lightweight, experimental tool designed to help obscure keyboard input from basic keyloggers by injecting misleading keystrokes into the input stream. It is not a complete security solution, but rather a utility for obfuscation in certain situations.

![KeyloggerGarbageScreenshot](https://github.com/user-attachments/assets/2496e664-b4c9-443d-8225-3af6da9ddc19)

🔍 Overview
KeyloggerGarbage attempts to confuse basic keylogging tools by:

Garbage Keystroke Injection – Random fake keystrokes that appear to keyloggers but don’t affect actual input

Shadow Typing – Injects characters that resemble real input

Special Key Injection – Sends occasional special keys (like arrow or function keys)

Variable Timing – Adds randomness to input timing to avoid predictable patterns

🛡️ Threats KeyloggerGarbage Helps Against
These are the most common types of keyloggers and scenarios where your tool can actually make a difference:

✅ Effectively Obfuscated By KeyloggerGarbage
Global hook-based keyloggers
(using SetWindowsHookEx to capture keystrokes)

Polling-based keyloggers
(using GetAsyncKeyState() or similar to check key states repeatedly)

Basic hardware keyloggers
(USB/PS2 inline devices that log raw input without analysis)

Amateur keyloggers from GitHub or pastebin-type sources
(quick DIY keylogger scripts)

Phishing payloads
(keyloggers bundled in fake game cracks, cheat engines, keygens, etc.)

Freeware spy tools
(simple "monitoring" programs disguised as productivity tools)

Parental control/snooping apps
(used to track kids, spouses, or roommates)

Employee monitoring software
(basic surveillance tools some companies use to track keystrokes)

Less sophisticated RATs
(Remote Access Trojans that include keylogging but use common techniques)

Malware with low-level obfuscation
(mass-distributed malware that adds a keylogger as one of its functions)


⚙️ System Requirements
Windows 7, 8, 10, or 11

.NET Framework 4.7.1 or higher

Administrator privileges (required for keyboard hooks)

🚀 Getting Started
Download the ZIP from the Releases page

Extract all files to a folder of your choice

Right-click KeyloggerGarbage.exe and choose "Run as Administrator"

💡 Usage Tips
Click "Enable Protection" to begin injecting keystroke obfuscation

Use the protection level slider to adjust how aggressive the injections are

The Live Input window shows a mix of real and fake keys

The Activity Log records protection activity and can be exported

🧪 Effectiveness
Effective Against:

Basic software keyloggers using Windows hooks

Simple hardware loggers without timing intelligence

Limited Against:

Advanced malware using timing, AI, or multi-capture methods

Not Effective Against:

Screen capture tools, form grabbers, memory-reading malware, or highly sophisticated keyloggers

⚠️ Limitations
This is a utility, not a full security application. It does not:

Detect keyloggers

Prevent infection

Replace good security practices

Protect against screen grabs or clipboard sniffing

Use it only when necessary and in combination with trusted security software.

🛠️ Troubleshooting
App won't launch? Run as Administrator and check .NET version

Typing feels off or buggy? Lower the protection level

App causes lag? Disable protection when not needed

📜 License
MIT License — use freely, no data is collected or transmitted.
