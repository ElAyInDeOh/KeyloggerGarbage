using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace KeyloggerDefender
{
    public partial class Form1 : Form
    {
        // P/Invoke declarations for Windows API functions
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        // Hook constants
        private const int WH_KEYBOARD_LL = 13; // Low-level keyboard hook
        private const int WM_KEYDOWN = 0x0100; // Key down message
        private const int WM_KEYUP = 0x0101; // Key up message
        private const uint KEYEVENTF_KEYUP = 0x0002; // Key up flag
                                                     // Keyboard event constants
        private const int KEYEVENTF_KEYDOWN = 0x0000;

        // Delegate for the hook procedure
        public delegate int HookProc(int nCode, int wParam, IntPtr lParam);

        private HookProc _hookProc; // Global hook procedure
        private IntPtr _hookID = IntPtr.Zero; // Hook ID
        private bool _protectionActive = false; // To track protection state
        private Random _random = new Random(); // To generate random numbers for garbage input
        private bool _isCurrentKeyReal = true; // Flag to distinguish real vs. garbage keys
        private Thread _garbageThread; // Thread for random garbage injection
        private bool _garbageThreadRunning = false; // Flag to control garbage thread
        private readonly object _lockObject = new object(); // For thread synchronization
        private DateTime _lastGarbageTime = DateTime.MinValue; // Track timing of garbage insertion

        // Key event structure to store keystroke data
        private class KeyEvent
        {
            public char Key { get; set; }
            public DateTime Time { get; set; }
            public bool IsGarbage { get; set; }
            public string WindowTitle { get; set; }
        }

        // List to store activity log entries
        private List<string> _activityLog = new List<string>();

        // Allowed key codes for garbage injection - avoid system keys
        private readonly byte[] _allowedKeyCodes = {
            // Letters
            65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77,
            78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90,
            // Numbers
            48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
            // Punctuation (limited set to avoid system shortcuts)
            190, 188, 186, 222, 219, 221, 220
        };

        public Form1()
        {
            InitializeComponent();
            _hookProc = new HookProc(HookCallback);  // Initialize hook procedure

            // Set tooltips
            toolTip1.SetToolTip(protectionTrackBar, "Adjust garbage keystroke frequency (1=less, 10=more)");
            toolTip1.SetToolTip(protectionCheckbox, "Enable or disable keystroke protection");

            // Update status label
            UpdateStatusLabel();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // Don't automatically start protection
            _hookID = IntPtr.Zero;
            LogActivity("Application started. Protection inactive.");
        }

        private void protectionCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _protectionActive = protectionCheckbox.Checked;

            if (_protectionActive)
            {
                // Start protection if not already active
                if (_hookID == IntPtr.Zero)
                {
                    _hookID = SetWindowsHookEx(WH_KEYBOARD_LL, _hookProc, IntPtr.Zero, 0);
                    StartGarbageThread();
                    LogActivity("Protection activated");
                }
            }
            else
            {
                // Stop protection if active
                if (_hookID != IntPtr.Zero)
                {
                    StopGarbageThread();
                    UnhookWindowsHookEx(_hookID);
                    _hookID = IntPtr.Zero;
                    LogActivity("Protection deactivated");
                }
            }

            // Update checkbox text
            protectionCheckbox.Text = _protectionActive ?
                "Disable Protection (Active)" :
                "Enable Protection";

            // Update status label
            UpdateStatusLabel();
        }

        private int HookCallback(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if (wParam == WM_KEYDOWN || wParam == WM_KEYUP)
                {
                    // Get the key information
                    int vkCode = Marshal.ReadInt32(lParam);

                    // Track and display real keystrokes
                    if (_isCurrentKeyReal)
                    {
                        if (wParam == WM_KEYDOWN)
                        {
                            char key = (char)vkCode;
                            string windowTitle = GetActiveWindowTitle();

                            // Log real key press
                            LogActivity($"Real key pressed: {key} in {windowTitle}");

                            // Add a debug message to check if this block is executing
                            Console.WriteLine($"Real key detected: {key}");

                            // Make sure we're updating the UI on the correct thread
                            if (!IsDisposed && IsHandleCreated)
                            {
                                BeginInvoke(new Action(() =>
                                {
                                    if (!IsDisposed && logBox != null)
                                    {
                                        logBox.AppendText(key.ToString());
                                        Console.WriteLine("UI updated with key: " + key);
                                    }
                                }));
                            }

                            // Process obfuscation methods only AFTER handling the real key
                            if (_protectionActive)
                            {
                                // Store the current key for potential use in obfuscation
                                char currentKey = key;

                                // Apply obfuscation techniques on a separate thread
                                ThreadPool.QueueUserWorkItem(_ =>
                                {
                                    // Add garbage keystrokes
                                    if (_random.Next(100) < 70)
                                    {
                                        int numGarbageKeys = _random.Next(1, 4);
                                        for (int i = 0; i < numGarbageKeys; i++)
                                        {
                                            Thread.Sleep(_random.Next(5, 70));
                                            InjectGarbageKey();
                                        }
                                    }

                                    // Special keys
                                    if (_random.Next(100) < 30)
                                    {
                                        Thread.Sleep(_random.Next(10, 60));
                                        InjectSpecialKey();
                                    }

                                    // Shadow typing
                                    if (_random.Next(100) < 50)
                                    {
                                        Thread.Sleep(_random.Next(5, 30));
                                        ShadowTypeKey(currentKey);
                                    }
                                });
                            }
                        }

                        // Let the real keypress go through to the application
                        return CallNextHookEx(_hookID, nCode, wParam, lParam);
                    }
                    else
                    {
                        // For garbage keys, don't pass them to applications
                        return 1; // Indicate that the key was handled
                    }
                }
            }

            // Pass the hook on to the next hook in the chain
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void UpdateStatusLabel()
        {
            statusLabel.Text = _protectionActive ? "Protection: Active" : "Protection: Inactive";
            statusLabel.ForeColor = _protectionActive ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }

        private void StartGarbageThread()
        {
            if (_garbageThreadRunning) return;

            _garbageThreadRunning = true;
            _garbageThread = new Thread(GarbageThreadProc)
            {
                IsBackground = true,
                Name = "Garbage Key Injection Thread"
            };
            _garbageThread.Start();

            LogActivity("Random keystroke injection thread started");
        }

        private void StopGarbageThread()
        {
            if (!_garbageThreadRunning) return;

            // Signal the thread to stop
            _garbageThreadRunning = false;

            // Try to join with a short timeout
            if (_garbageThread != null && _garbageThread.IsAlive)
            {
                // Don't wait too long to avoid freezing
                if (!_garbageThread.Join(300))
                {
                    // If can't join cleanly, we'll let it terminate naturally
                    // The thread will exit eventually when it checks _garbageThreadRunning
                    Debug.WriteLine("Garbage thread didn't exit cleanly");
                }
            }

            LogActivity("Random keystroke injection thread stopped");
        }

        private void UpdateLogBoxWithGarbageKey(char key)
        {
            if (IsDisposed || !IsHandleCreated) return;

            BeginInvoke(new Action(() =>
            {
                if (!IsDisposed && logBox != null)
                {
                    // Display garbage keys exactly like real keys - no visual distinction
                    logBox.AppendText(key.ToString());
                    Console.WriteLine("UI updated with garbage key: " + key);
                }
            }));
        }

        private void InjectSpecialKey()
        {
            _isCurrentKeyReal = false;

            // Array of virtual key codes for special keys
            byte[] specialKeys = {
                0x25, // LEFT ARROW
                0x26, // UP ARROW
                0x27, // RIGHT ARROW
                0x28, // DOWN ARROW
                0x70, // F1
                0x71, // F2
                0x72, // F3
                0x73, // F4
                0x13, // PAUSE
                0x91, // SCROLL LOCK
            };

            byte randomKey = specialKeys[_random.Next(specialKeys.Length)];

            // Inject a special keypress
            keybd_event(randomKey, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            keybd_event(randomKey, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);

            // Update the log box with the special key
            if (randomKey >= 0x25 && randomKey <= 0x28)
            {
                // Arrow keys
                string[] arrowNames = { "←", "↑", "→", "↓" };
                UpdateLogBoxWithGarbageKey(arrowNames[randomKey - 0x25][0]);
            }
            else if (randomKey >= 0x70 && randomKey <= 0x73)
            {
                // F1-F4 keys
                UpdateLogBoxWithGarbageKey('F');
            }
            else
            {
                // Other special keys - use generic representation
                UpdateLogBoxWithGarbageKey('•');
            }

            LogActivity($"Injected special key: {randomKey}");
            _isCurrentKeyReal = true;
        }

        private void ShadowTypeKey(char originalKey)
        {
            _isCurrentKeyReal = false;

            // Options for shadow typing:
            // 1. Repeat the same key
            // 2. Type a nearby key on the keyboard
            // 3. Type a lookalike character

            char shadowKey = originalKey;
            int method = _random.Next(3);

            if (method == 0)
            {
                // Just repeat the key
            }
            else if (method == 1)
            {
                // Get a nearby key on QWERTY layout
                Dictionary<char, char[]> nearbyKeys = GetNearbyKeysMap();

                if (nearbyKeys.ContainsKey(char.ToLower(originalKey)))
                {
                    char[] options = nearbyKeys[char.ToLower(originalKey)];
                    shadowKey = options[_random.Next(options.Length)];
                    if (char.IsUpper(originalKey)) shadowKey = char.ToUpper(shadowKey);
                }
            }
            else
            {
                // Use a lookalike character
                Dictionary<char, char[]> lookalikes = GetLookalikeMap();

                if (lookalikes.ContainsKey(char.ToLower(originalKey)))
                {
                    char[] options = lookalikes[char.ToLower(originalKey)];
                    shadowKey = options[_random.Next(options.Length)];
                }
            }

            // Inject the shadow key
            byte vkCode = (byte)VkKeyScan(shadowKey);
            keybd_event(vkCode, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            keybd_event(vkCode, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);

            // Update the log box with the shadow key
            UpdateLogBoxWithGarbageKey(shadowKey);

            LogActivity($"Shadow typed: {shadowKey} after {originalKey}");
            _isCurrentKeyReal = true;
        }

        private Dictionary<char, char[]> GetNearbyKeysMap()
        {
            // Simplified QWERTY keyboard neighboring keys
            return new Dictionary<char, char[]>
            {
                {'q', new[] {'w', 'a', '1', '2'}},
                {'w', new[] {'q', 'e', 'a', 's', '2', '3'}},
                {'e', new[] {'w', 'r', 's', 'd', '3', '4'}},
                {'r', new[] {'e', 't', 'd', 'f', '4', '5'}},
                {'t', new[] {'r', 'y', 'f', 'g', '5', '6'}},
                {'y', new[] {'t', 'u', 'g', 'h', '6', '7'}},
                {'u', new[] {'y', 'i', 'h', 'j', '7', '8'}},
                {'i', new[] {'u', 'o', 'j', 'k', '8', '9'}},
                {'o', new[] {'i', 'p', 'k', 'l', '9', '0'}},
                {'p', new[] {'o', '[', 'l', ';', '0'}},
                {'a', new[] {'q', 'w', 's', 'z'}},
                {'s', new[] {'w', 'e', 'a', 'd', 'z', 'x'}},
                {'d', new[] {'e', 'r', 's', 'f', 'x', 'c'}},
                {'f', new[] {'r', 't', 'd', 'g', 'c', 'v'}},
                {'g', new[] {'t', 'y', 'f', 'h', 'v', 'b'}},
                {'h', new[] {'y', 'u', 'g', 'j', 'b', 'n'}},
                {'j', new[] {'u', 'i', 'h', 'k', 'n', 'm'}},
                {'k', new[] {'i', 'o', 'j', 'l', 'm'}},
                {'l', new[] {'o', 'p', 'k', ';'}},
                {'z', new[] {'a', 's', 'x'}},
                {'x', new[] {'s', 'd', 'z', 'c'}},
                {'c', new[] {'d', 'f', 'x', 'v'}},
                {'v', new[] {'f', 'g', 'c', 'b'}},
                {'b', new[] {'g', 'h', 'v', 'n'}},
                {'n', new[] {'h', 'j', 'b', 'm'}},
                {'m', new[] {'j', 'k', 'n'}}
            };
        }

        private Dictionary<char, char[]> GetLookalikeMap()
        {
            // Characters that look similar
            return new Dictionary<char, char[]>
            {
                {'a', new[] {'@', '4', 'à', 'á', 'â'}},
                {'b', new[] {'6', '8', 'ß'}},
                {'c', new[] {'(', '<', '©', 'ç'}},
                {'e', new[] {'3', 'è', 'é', 'ê', 'ë'}},
                {'g', new[] {'9', 'q'}},
                {'i', new[] {'1', '!', '|', 'í', 'ì', 'î', 'ï'}},
                {'l', new[] {'1', '|', '/', 'I'}},
                {'o', new[] {'0', 'ó', 'ò', 'ô', 'õ', 'ö'}},
                {'s', new[] {'5', '$', '§'}},
                {'t', new[] {'+', '†'}},
                {'u', new[] {'v', 'µ', 'ü', 'û', 'ú', 'ù'}},
                {'v', new[] {'u', 'ν'}},
                {'x', new[] {'×', '✗', '✘'}},
                {'y', new[] {'ÿ', 'ý'}}
            };
        }

        // P/Invoke for VkKeyScan
        [DllImport("user32.dll")]
        static extern short VkKeyScan(char ch);

        private void GarbageThreadProc()
        {
            while (_garbageThreadRunning)
            {
                try
                {
                    int baseDelay;

                    // Get the value safely from the UI thread with a timeout
                    if (InvokeRequired && !IsDisposed)
                    {
                        IAsyncResult result = BeginInvoke(new Func<int>(() =>
                            IsDisposed ? 5 : 11 - protectionTrackBar.Value));

                        // Use a timeout to prevent deadlocks
                        if (result.AsyncWaitHandle.WaitOne(500))
                        {
                            baseDelay = (int)EndInvoke(result);
                        }
                        else
                        {
                            // Default value if invoke times out
                            baseDelay = 5;
                        }
                    }
                    else if (IsDisposed)
                    {
                        // Exit thread if form is disposed
                        return;
                    }
                    else
                    {
                        baseDelay = 11 - protectionTrackBar.Value;
                    }

                    // Calculate delay based on protection level
                    int delay = _random.Next(baseDelay * 300, baseDelay * 1000);

                    // Sleep for the calculated delay
                    Thread.Sleep(delay);

                    // Only inject if still running and not too soon after last injection
                    if (_garbageThreadRunning &&
                        (DateTime.Now - _lastGarbageTime).TotalMilliseconds > 100)
                    {
                        // Use ThreadPool to avoid blocking this thread
                        ThreadPool.QueueUserWorkItem(_ => InjectGarbageKey());
                    }
                }
                catch (ObjectDisposedException)
                {
                    // Form was disposed, exit thread
                    return;
                }
                catch (InvalidOperationException)
                {
                    // UI thread unavailable, wait and retry
                    Thread.Sleep(500);
                }
                catch (Exception)
                {
                    // Any other exception, wait a bit before continuing
                    Thread.Sleep(200);
                }
            }
        }

        private void InjectGarbageKey()
        {
            _isCurrentKeyReal = false;

            // Track the time of the last garbage injection
            _lastGarbageTime = DateTime.Now;

            // Use random character generation strategy
            int strategy = _random.Next(4);
            char garbageChar;

            if (strategy == 0)
            {
                // Random letter (A-Z)
                garbageChar = (char)(_random.Next(26) + 'a');
                // Randomly capitalize
                if (_random.Next(2) == 1)
                    garbageChar = char.ToUpper(garbageChar);
            }
            else if (strategy == 1)
            {
                // Random number (0-9)
                garbageChar = (char)(_random.Next(10) + '0');
            }
            else if (strategy == 2)
            {
                // Common punctuation
                char[] punctuation = { '.', ',', ';', ':', '!', '?', '-', '_', '=', '+', '/', '*' };
                garbageChar = punctuation[_random.Next(punctuation.Length)];
            }
            else
            {
                // Context-aware garbage (mimicking common letters in English)
                char[] commonLetters = { 'e', 't', 'a', 'o', 'i', 'n', 's', 'h', 'r', 'd', 'l', 'u' };
                garbageChar = commonLetters[_random.Next(commonLetters.Length)];
                // Randomly capitalize
                if (_random.Next(3) == 0)
                    garbageChar = char.ToUpper(garbageChar);
            }

            // Convert to virtual key code
            byte vkCode = (byte)VkKeyScan(garbageChar);

            // Inject the keypress
            keybd_event(vkCode, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            keybd_event(vkCode, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);

            // Update the log box with the garbage key (no special formatting)
            UpdateLogBoxWithGarbageKey(garbageChar);

            LogActivity($"Injected garbage key: {garbageChar}");
            _isCurrentKeyReal = true;
        }

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, buff, nChars) > 0)
            {
                return buff.ToString();
            }
            return "Unknown";
        }

        private void LogActivity(string message)
        {
            if (IsDisposed) return;

            if (InvokeRequired)
            {
                try
                {
                    // Use BeginInvoke to avoid blocking
                    BeginInvoke(new Action<string>(LogActivity), message);
                }
                catch (ObjectDisposedException)
                {
                    // Form is disposing, ignore
                }
                catch (InvalidOperationException)
                {
                    // Handle when invoke isn't possible
                }
                return;
            }

            // Add timestamp to log
            string logEntry = $"[{DateTime.Now:HH:mm:ss.fff}] {message}";
            _activityLog.Add(logEntry);

            // Update activity log display if still valid
            if (!IsDisposed && activityListBox != null)
            {
                activityListBox.Items.Add(logEntry);

                // Keep only last 100 entries for efficiency
                while (activityListBox.Items.Count > 100)
                {
                    activityListBox.Items.RemoveAt(0);
                }

                // Auto-scroll to the bottom
                activityListBox.TopIndex = Math.Max(0, activityListBox.Items.Count - 1);
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Export activity log
                using (StreamWriter writer = new StreamWriter("protection_activity_log.txt"))
                {
                    writer.WriteLine("=== ANTI-KEYLOGGER PROTECTION LOG ===");
                    writer.WriteLine($"Generated: {DateTime.Now}");
                    writer.WriteLine();

                    // Write all log entries
                    foreach (var entry in _activityLog)
                    {
                        writer.WriteLine(entry);
                    }
                }

                MessageBox.Show("Activity log exported to: protection_activity_log.txt",
                    "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting log: {ex.Message}",
                    "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Stop protection first
                _protectionActive = false;

                // Stop garbage thread with limited wait time
                StopGarbageThread();

                // Clean up hooks
                if (_hookID != IntPtr.Zero)
                {
                    UnhookWindowsHookEx(_hookID);
                    _hookID = IntPtr.Zero;
                    LogActivity("Keyboard hook removed");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during form closing: {ex.Message}");
                // Continue closing anyway
            }
        }
    }
}