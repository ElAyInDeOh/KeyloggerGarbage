🛡️ KeyloggerGarbage

KeyloggerGarbage is a lightweight, experimental tool designed to obscure keyboard input from basic keyloggers by injecting misleading keystrokes into the input stream. It is not a complete security solution, but rather a focused obfuscation utility meant for use in specific situations.

![KeyloggerGarbageScreenshot](https://github.com/user-attachments/assets/f1a18038-c211-4a42-a93e-189f3749ac63)


🔍 Overview
KeyloggerGarbage attempts to confuse common keylogging tools by using several layered techniques:

Garbage Keystroke Injection – Sends random fake keystrokes that appear to keyloggers but don’t affect actual input.

Shadow Typing – Inputs characters that resemble what you're typing to further distort logs.

Special Key Injection – Occasionally sends non-character keys (like arrow or function keys) to break up patterns.

Variable Timing – Adds randomized delays between injections to avoid predictable input behavior.

🛡️ Threat Coverage
These are the most common types of keyloggers and surveillance scenarios where KeyloggerGarbage can help:

✅ Effectively Obfuscates:
Global hook-based keyloggers (SetWindowsHookEx)

Polling-based keyloggers (GetAsyncKeyState, etc.)

Basic hardware keyloggers (USB/PS2 inline devices without timing analysis)

Amateur scripts from GitHub or Pastebin

Phishing payload keyloggers (bundled with cracks, cheats, keygens)

Freeware spy tools disguised as productivity apps

Parental control/snooping tools

Basic employee monitoring software

Less sophisticated Remote Access Trojans (RATs)

Malware using low-level, non-AI-based keystroke logging

⚙️ System Requirements
Windows 7 / 8 / 10 / 11

.NET Framework 4.7.1 or higher

Administrator privileges (required for low-level keyboard hooks)

🚀 Getting Started
Download the ZIP from the Releases page

Extract all files to a folder of your choice

Right-click KeyloggerGarbage.exe and choose "Run as Administrator"

💡 How to Use
Click "Enable Protection" to begin obfuscating your keyboard input

Adjust the Protection Level slider to control injection frequency

Lower = better performance, less protection

Higher = more protection, possible input delay

The Live Input window shows a real-time mix of real and fake keystrokes

Use Export Log to save activity data for analysis

🧪 Effectiveness
🟢 Effective Against:
Basic software keyloggers using Windows APIs

Simple hardware loggers without timing intelligence

🟡 Limited Protection Against:
Advanced malware using timing analysis, AI, or multi-capture strategies

🔴 Not Effective Against:
Screen capture malware

Form grabbers (data interception after input is processed)

Memory-reading malware

Sophisticated keyloggers with pattern recognition or heuristics

⚠️ Limitations & Warnings
❌ Does not detect or remove keyloggers

❌ Does not protect against screen recording or clipboard sniffers

❌ Should not replace trusted security software

⚠️ May cause lag or interfere with certain applications (e.g., games or software with custom keyboard input)

Use only when needed — and always in combination with good security habits.

🛠️ Troubleshooting
Problem	Solution
App won't launch	Run as Administrator and ensure .NET Framework is installed
Input feels buggy or delayed	Lower the protection level slider
Performance issues or slowdowns	Disable protection when not typing sensitive information
📜 License
MIT License — Free to use, modify, and distribute.
Privacy Note: KeyloggerGarbage collects no user data. All activity logs are stored locally and never transmitted.

