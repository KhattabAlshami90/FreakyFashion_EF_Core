namespace FreakyFashion_EF_Core.Data
{
    class Logo
    {

        public static void ClearScreen()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            string logo = @"
______              _         ______        _     _             
|  ___|            | |        |  ___|      | |   (_)            
| |_ _ __ ___  __ _| | ___   _| |_ __ _ ___| |__  _  ___  _ __  
|  _| '__/ _ \/ _` | |/ / | | |  _/ _` / __| '_ \| |/ _ \| '_ \ 
| | | | |  __/ (_| |   <| |_| | || (_| \__ \ | | | | (_) | | | |
\_| |_|  \___|\__,_|_|\_\\__, \_| \__,_|___/_| |_|_|\___/|_| |_|
                          __/ |                                 
                         |___/                                  ";

            Console.Clear();
            Console.WriteLine(logo);
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
