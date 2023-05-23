using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace SoftEtherConfiguration
{
    public class Configuration
    {
        #region Head
        /// <summary>
        /// Logger de la classe Configuration
        /// </summary>
        private static readonly Logger appLog = LogManager.GetLogger("ConfigurationLogger");

        #region Membres
        private List<string> ConfigurationText { get; set; }
        private List<string> ConfigurationKey { get; set; }
        private int Pointer { get; set; }
        private List<string> blockSawmill { get; set; }
        private List<string> blockUser { get; set; }
        #endregion

        #region Dictionnaires
        public Dictionary<string, SawMill> SawMills { get; set; }
        public Dictionary<string, User> Users { get; set; }
        #endregion
        #endregion

        /// <summary>
        /// Lecture du fichier et ajout dans les dictionnaires
        /// </summary>
        /// <param name="pathName">Chemin d'accès vers le fichier</param>
        public Configuration(string pathName)
        {
            appLog.Info($"Fichier : {pathName}");
            ConfigurationText = (File.ReadAllLines(pathName)).ToList();

            Users = GetUsers();
            SawMills = GetSawMills();
            ListUsers();
            ListSawMills();
            ListResults();
        }

        #region Logs
        /// <summary>
        /// [LOGS] Liste des utilisateurs par scieries.
        /// </summary>
        private void ListResults()
        {
            appLog.Info("\n\nAffichage des résultats");
            foreach(var sawMill in SawMills)
            {
                appLog.Info($"\tScierie : {sawMill.Value.SawMillName}");
                foreach(var user in sawMill.Value.Users)
                {
                    appLog.Info($"\tUtilisateur : {user.Value.UserName}");
                }
            }
        }

        /// <summary>
        /// [LOGS] Liste des utilisateurs.
        /// </summary>
        private void ListUsers()
        {
            appLog.Info($"\n\nAffichage des utilisateurs ({Users.Count})");


                foreach (var user in Users)
                {
                    appLog.Info($"\tUtilisateur : {user.Value.UserName}");
                }
    
        }

        /// <summary>
        /// [LOGS] Liste des scieries.
        /// </summary>
        private void ListSawMills()
        {
            appLog.Info($"\n\nAffichage des scieries ({SawMills.Count})");


            foreach (var sawMill in SawMills)
            {
                appLog.Info($"\tScierie : {sawMill.Value.SawMillName}");
            }

        }

        /// <summary>
        /// Convertie la mémoire de List à string[].
        /// </summary>
        /// <returns>Donne la mémoire sous forme string[]</returns>
        public object WriteText()
        {
            return ConfigurationText.ToArray();
        }
        #endregion

        #region GetClasses
        /// <summary>
        /// Récupère les utilisateurs dans le fichier envoyé.
        /// </summary>
        /// <returns>Donne le dictionnaire des utilisateurs</returns>
        private Dictionary<string, User> GetUsers()
        {
            var users = new Dictionary<string, User>();

            SeekStarting();
            User newUser;
            while ((newUser = GetNextUser()) != null)
            {
                if (!users.ContainsKey(newUser.UserName))
                {
                    users[newUser.UserName] = newUser;
                }
            }

            return users;
        }

        /// <summary>
        /// Récupère les scieries dans le fichier envoyé.
        /// </summary>
        /// <returns>Donne le dictionnaire des scieries</returns>
        private Dictionary<string, SawMill> GetSawMills()
        {
            var sawMills = new Dictionary<string, SawMill>();

            SeekStarting();
            SawMill newSawMill;
            while ((newSawMill = GetNextSawMill()) != null)
            {
                if (!sawMills.ContainsKey(newSawMill.SawMillName))
                {
                    sawMills[newSawMill.SawMillName] = newSawMill;
                }
            }

            return sawMills;
        }
        #endregion

        #region Add()

        /// <summary>
        /// Ajoute un nouvel utilisateur.
        /// </summary>
        /// <param name="UserName">Identifiant unique (ex : nom.p)</param>
        /// <param name="Key">Clé de certificat</param>
        /// <param name="Name">Nom complet</param>
        /// <returns>Donne l'utilisateur ajouté dans le dictionnaire.</returns>
        public User AddUser(string UserName, string Key, string Name)
        {
            ConfigurationKey = (File.ReadAllLines(Key)).ToList();
            var p = 1;
            var text = "";

            while (p < (ConfigurationKey.Count - 1))
            {
                text += ConfigurationKey[p];
                p++;
            }

            User newUser = new User();

            newUser.UserName = UserName;
            newUser.Key = text;
            newUser.Name = Name;

            if (!Users.ContainsKey(newUser.UserName))
                Users.Add(UserName, newUser);

            return newUser;
        }

        /// <summary>
        /// Ajoute une nouvelle scierie.
        /// </summary>
        /// <param name="name">Indentifiant unique</param>
        /// <returns>Donne la scierie ajoutée dans le dictionnaire.</returns>
        public SawMill AddSawmill(string name)
        {
            SawMill newSawmill = new SawMill();

            newSawmill.SawMillName = name;

            if (!SawMills.ContainsKey(newSawmill.SawMillName))
                SawMills.Add(name, newSawmill);

            return newSawmill;
        }

        /// <summary>
        /// Ajoute un bloc scierie dans la mémoire du texte.
        /// </summary>
        /// <param name="nameSawmill">Indentifiant unique de la scierie</param>
        public void AddTextSawmill(string nameSawmill)
        {
            GetBlockSawmill();
            SeekStarting();
            GetNext("RadiusConvertAllMsChapv2AuthRequestToEap", 1);
            GetPrevious("declare", 0, true);
            var p = Pointer + 1;

            ConfigurationText.InsertRange(p, blockSawmill);

            Pointer = p;

            var unixTimestamp = Convert.ToString(DateTimeOffset.Now.ToUnixTimeSeconds());

            ReplaceNext("declare", 0, nameSawmill);
            ReplaceNext("CreatedTime", 1, unixTimestamp + "000");
            ReplaceNext("LastCommTime", 1, "0");
            ReplaceNext("LastLoginTime", 1, "0");
            ReplaceNext("NumLogin", 1, "0");
        }

        /// <summary>
        /// Ajoute un bloc utilisateur dans un bloc scierie, dans la mémoire du texte.
        /// </summary>
        /// <param name="nameSawmill">Identifiant de la scierie ou sera l'utilisateur</param>
        /// <param name="user">Objet utilisateur</param>
        public void AddTextUser(string nameSawmill, User user)
        {
            //try
            //{
                if (!SawMills[nameSawmill].Users.ContainsKey(user.UserName))
                {
                    GetBlockUser();
                    SeekStarting();
                    GetLine(nameSawmill, 1);
                    GetNextUserList("UserList", 1);
                    var p = Pointer + 1;

                    ConfigurationText.InsertRange(p, blockUser);

                    Pointer = p;

                    var unixTimestamp = Convert.ToString(DateTimeOffset.Now.ToUnixTimeSeconds());

                    ReplaceNext("declare", 0, user.UserName);
                    ReplaceNext(new string[] { "AuthUserCert", "AuthPassword" }, 1, user.Key);
                    ReplaceNext("CreatedTime", 1, unixTimestamp + "000");
                    ReplaceNext("LastLoginTime", 1, "0");
                    ReplaceNext("NumLogin", 1, "0");
                    ReplaceNext("RealName", 1, user.Name);
                    ReplaceNext("UpdatedTime", 1, unixTimestamp + "000");
                    ReplaceNext("BroadcastBytes", 1, "0");
                    ReplaceNext("BroadcastCount", 1, "0");
                    ReplaceNext("UnicastBytes", 1, "0");
                    ReplaceNext("UnicastCount", 1, "0");
                    ReplaceNext("BroadcastBytes", 1, "0");
                    ReplaceNext("BroadcastCount", 1, "0");
                    ReplaceNext("UnicastBytes", 1, "0");
                    ReplaceNext("UnicastCount", 1, "0");
                //}
            }
        }
        #endregion

        #region Models
        /// <summary>
        /// Récupère un bloc modèle d'utilisateur dans la mémoire.
        /// </summary>
        private void GetBlockUser()
        {
            SeekStarting();
            GetLine("LBL_TEST", 1);
            GetNext(new string[] { "AuthUserCert", "AuthPassword" }, 1);
            GetPrevious("declare", 0, true);
            var startBlock = Pointer + 1;
            GetNext(new string[] { "AuthUserCert", "AuthPassword" }, 1);
            GetNext(new string[] { "AuthUserCert", "AuthPassword" }, 1);
            GetPrevious("declare", 0, true);
            var endBlock = Pointer + 1;

            blockUser = new List<string>();
            for (int i = startBlock; i < endBlock; i++)
            {
                blockUser.Add(ConfigurationText[i]);
            }
        }

        /// <summary>
        /// Récupère un bloc modèle de scierie dans la mémoire.
        /// </summary>
        private void GetBlockSawmill()
        {
            SeekStarting();
            GetLine("LBL_TEST", 1);
            var startBlock = Pointer;
            GetNext("RadiusConvertAllMsChapv2AuthRequestToEap", 1);
            GetNext("RadiusConvertAllMsChapv2AuthRequestToEap", 1);
            GetPrevious("declare", 0, true);
            var endBlock = Pointer + 1;

            blockSawmill = new List<string>();
            for (int i = startBlock; i < endBlock; i++)
            {
                blockSawmill.Add(ConfigurationText[i]);
            }

            RemoveBlockUser(blockSawmill);
        }
        #endregion

        #region EditText
        /// <summary>
        /// Met la position du pointeur à 0 (début de la mémoire).
        /// </summary>
        private void SeekStarting()
        {
            Pointer = 0;
        }

        /// <summary>
        /// Retire le bloc utilisateur du bloc scierie de la mémoire.
        /// </summary>
        /// <param name="blockSawmill">Bloc de la scierie</param>
        private void RemoveBlockUser(List<string> blockSawmill)
        {
            var p = 0;

            int start = 0;
            int end = 0;

            bool foundStart = false;
            bool foundEnd = false;
            while (p < blockSawmill.Count && !foundStart)
            {
                var words = blockSawmill[p].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length >= 2)
                {
                    if (words[1] == "UserList")
                    {
                        foundStart = true;
                        start = p + 2;
                    }
                }
                p++;
            }

            while (p < blockSawmill.Count && !foundEnd)
            {
                var words = blockSawmill[p].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length >= 2)
                {
                    if (words[0] == "\t\t\tdeclare")
                    {
                        foundEnd = true;
                        end = p - 2;
                    }
                }
                p++;
            }

            var nb = end - start;

            blockSawmill.RemoveRange(start, nb);

            return;
        }

        /// <summary>
        /// Remplace le mot désiré placé après la position du pointeur.
        /// </summary>
        /// <param name="previousWord">Mot qui précéde le mot qui sera remplacé</param>
        /// <param name="researchNumber">Position du mot précédent sur la ligne</param>
        /// <param name="replaceWord">Nouveau mot qui remplacera l'ancien</param>
        private void ReplaceNext(string previousWord, int researchNumber, string replaceWord)
        {
            var p = Pointer;
            bool found = false;
            while (p < ConfigurationText.Count && !found)
            {
                var words = ConfigurationText[p].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length >= 2)
                {
                    if (words[researchNumber] == previousWord)
                    {
                        found = true;
                        words[researchNumber + 1] = replaceWord;
                        ConfigurationText[p] = "";
                        foreach (var word in words)
                        {
                            ConfigurationText[p] += word + " ";
                        }
                        Pointer = p;
                    }
                }
                p++;
            }
            return;
        }

        /// <summary>
        /// Remplace le mot désiré placé après la position du pointeur.
        /// </summary>
        /// <param name="previousWord">Mot qui précéde le mot qui sera remplacé</param>
        /// <param name="researchNumber">Position du mot précédent sur la ligne</param>
        /// <param name="replaceWord">Nouveau mot qui remplacera l'ancien</param>
        private void ReplaceNext(string[] previousWord, int researchNumber, string replaceWord)
        {
            var p = Pointer;
            bool found = false;
            while (p < ConfigurationText.Count && !found)
            {
                var words = ConfigurationText[p].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length >= 2)
                {
                    foreach (var n in previousWord)
                    {
                        if (words[researchNumber] == n)
                        {
                            found = true;
                            words[researchNumber + 1] = replaceWord;
                            ConfigurationText[p] = "";
                            foreach (var word in words)
                            {
                                ConfigurationText[p] += word + " ";
                            }
                            Pointer = p;
                        }
                    }
                }
                p++;
            }
            return;
        }

        /// <summary>
        /// Initialise la position du pointeur sur la ligne du mot suivant recherché.
        /// </summary>
        /// <param name="researchWord">Mot recherché</param>
        /// <param name="researchNumber">Position du mot recherché sur la ligne</param>
        private void GetLine(string researchWord, int researchNumber)
        {
            var p = Pointer;
            bool found = false;
            while (p < ConfigurationText.Count && !found)
            {
                var words = ConfigurationText[p].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length >= 2)
                {
                    if (words[researchNumber] == researchWord)
                    {
                        found = true;
                        Pointer = p;
                    }
                }
                p++;
            }
            return;
        }
        #endregion

        #region GetNext()
        /// <summary>
        /// Obtient la valeur qui suit le mot recherché en allant en avant sur le fichier.
        /// </summary>
        /// <param name="researchWord">Mot recherché</param>
        /// <param name="researchNumber">Position du mot sur la ligne</param>
        /// <returns>Donne le mot qui suis le recherché</returns>
        private string GetNext(string researchWord, int researchNumber)
        {
            var p = Pointer;
            string x = "";
            bool found = false;
            //appLog.Debug($"\tGetNext(string) :");
            while (p < ConfigurationText.Count && !found)
            {
                //appLog.Debug($"\t\tGetNext(string) : PASSAGE");
                var words = ConfigurationText[p].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length >= 2)
                {
                    if (words[researchNumber] == researchWord)
                    {
                        x = words[researchNumber + 1];
                        found = true;
                    }
                }
                p++;
            }

            Pointer = p;
            //appLog.Warn($"Valeur de p : {p}");

            if (found)
                return x;
            else
                return null;
        }

        /// <summary>
        /// Obtient la valeur qui suit le mot recherché en allant en avant sur le fichier.
        /// </summary>
        /// <param name="researchWord">Mot recherché</param>
        /// <param name="researchNumber">Position du mot sur la ligne</param>
        /// <returns>Donne le mot qui suis le recherché</returns>
        private string GetNext(string[] researchWord, int researchNumber)
        {
            var p = Pointer;
            string x = "";
            bool found = false;
            //appLog.Debug($"\tGetNext(string[]) :");
            while (p < ConfigurationText.Count && !found)
            {
                //appLog.Debug($"\t\tGetNext(string[]) : PASSAGE");
                var words = ConfigurationText[p].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length >= 2)
                {
                    foreach (var n in researchWord)
                    {
                        if (words[researchNumber] == n)
                        {
                            x = words[researchNumber + 1];
                            found = true;
                        }
                    }
                }
                p++;
            }

            Pointer = p;
            //appLog.Warn($"Valeur de p : {p}");

            if (found)
                return x;
            else
                return null;
        }

        /// <summary>
        /// Obtient la valeur qui suit le mot recherché en allant en avant sur le fichier.
        /// </summary>
        /// <param name="researchWord">Mot recherché</param>
        /// <param name="researchNumber">Position du mot sur la ligne</param>
        /// <param name="pStart">Position du début de recherche</param>
        /// <param name="pEnd">Position de fin de recherche</param>
        /// <returns>Donne le mot qui suis le recherché</returns>
        private string GetNext(string researchWord, int researchNumber, int pStart, int pEnd)
        {
            var p = pStart;
            string x = "";
            bool found = false;
            //appLog.Debug($"\tGetNext(string) :");
            while (p < ConfigurationText.Count && !found && p <= pEnd)
            {
                //appLog.Debug($"\t\tGetNext(string) : PASSAGE");
                var words = ConfigurationText[p].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length >= 2)
                {
                    if (words[researchNumber] == researchWord)
                    {
                        x = words[researchNumber + 1];
                        found = true;
                    }
                }
                p++;
            }

            Pointer = p;
            //appLog.Warn($"Valeur de p : {p}");

            if (found)
                return x;
            else
                return null;
        }

        /// <summary>
        /// Obtient la valeur qui suit le mot recherché en allant en avant sur le fichier.
        /// </summary>
        /// <param name="researchWord">Mot recherché</param>
        /// <param name="researchNumber">Position du mot sur la ligne</param>
        /// <param name="pStart">Position du début de recherche</param>
        /// <param name="pEnd">Position de fin de recherche</param>
        /// <returns>Donne le mot qui suis le recherché</returns>
        private string GetNext(string[] researchWord, int researchNumber, int pStart, int pEnd)
        {
            var p = pStart;
            string x = "";
            bool found = false;
            //appLog.Debug($"\tGetNext(string[]) :");
            while (p < ConfigurationText.Count && !found && p <= pEnd)
            {
                //appLog.Debug($"\t\tGetNext(string[]) : PASSAGE");
                var words = ConfigurationText[p].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length >= 2)
                {
                    foreach (var n in researchWord)
                    {
                        if (words[researchNumber] == n)
                        {
                            x = words[researchNumber + 1];
                            found = true;
                        }
                    }
                }
                p++;
            }

            Pointer = p;
            //appLog.Warn($"Valeur de p : {p}");

            if (found)
                return x;
            else
                return null;
        }

        /// <summary>
        /// Initialise la position du pointeur sur l'utilisateur recherché en allant en avant sur le fichier.
        /// </summary>
        /// <param name="researchWord">Mot/Nom recherché</param>
        /// <param name="researchNumber">Position du mot recherché</param>
        private void GetNextUserList(string researchWord, int researchNumber)
        {
            var p = Pointer;
            bool found = false;
            while (p < ConfigurationText.Count && !found)
            {
                var words = ConfigurationText[p].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length >= 2)
                {
                    if (words[researchNumber] == researchWord)
                    {
                        found = true;
                    }
                }
                p++;
            }

            Pointer = p;

            return;
        }
        #endregion

        #region GetPrevious()

        /// <summary>
        /// Obtient la valeur qui suit le mot recherché en revenant en arrière sur la mémoire.
        /// </summary>
        /// <param name="researchWord">Mot recherché</param>
        /// <param name="researchNumber">Position du mot recherché</param>
        /// <returns>Donne le mot suivant à celui recherché</returns>
        private string GetPrevious(string researchWord, int researchNumber)
        {
            var p = Pointer;
            string x = "";
            bool found = false;
            //appLog.Debug($"\tGetPrevious(string) :");
            while (p < ConfigurationText.Count && p != 0 && !found)
            {
                //appLog.Debug($"\t\tGetPrevious(string) : PASSAGE");
                var words = ConfigurationText[p].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length >= 2)
                {
                    if (words[researchNumber] == researchWord)
                    {
                        x = words[researchNumber + 1];
                        found = true;
                    }
                }
                p--;
            }
            //appLog.Warn($"Valeur de p : {p}");

            if (found)
                return x;
            else
                return null;
        }

        /// <summary>
        /// Obtient la valeur qui suit le mot recherché en revenant en arrière sur la mémoire.
        /// </summary>
        /// <param name="researchWord">Mot recherché</param>
        /// <param name="researchNumber">Position du mot recherché</param>
        /// <param name="askPointer">True = initialise la position du pointeur</param>
        /// <returns>Donne le mot suivant à celui recherché</returns>
        private string GetPrevious(string[] researchWord, int researchNumber, bool askPointer)
        {
            var p = Pointer;
            string x = "";
            bool found = false;
            //appLog.Debug($"\tGetPrevious(string[]) :");
            while (p < ConfigurationText.Count && p != 0 && !found)
            {
                //appLog.Debug($"\t\tGetPrevious(string[]) : PASSAGE");
                var words = ConfigurationText[p].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length >= 2)
                {
                    foreach (var n in researchWord)
                    {
                        if (words[researchNumber] == n)
                        {
                            x = words[researchNumber + 1];
                            found = true;
                        }
                    }
                }
                p--;
            }
            //appLog.Warn($"Valeur de p : {p}");

            if (found)
            {
                if (askPointer)
                {
                    Pointer = p;
                    return x;
                }
                else
                    return x;
            }
            else
                return null;
        }

        /// <summary>
        /// Obtient la valeur qui suit le mot recherché en revenant en arrière sur la mémoire.
        /// </summary>
        /// <param name="researchWord">Mot recherché</param>
        /// <param name="researchNumber">Position du mot recherché</param>
        /// <param name="askPointer">True = initialise la position du pointeur</param>
        /// <returns>Donne le mot suivant à celui recherché</returns>
        private string GetPrevious(string researchWord, int researchNumber, bool askPointer)
        {
            var p = Pointer;
            string x = "";
            bool found = false;
            //appLog.Debug($"\tGetPrevious(string) :");
            while (p < ConfigurationText.Count && p != 0 && !found)
            {
                //appLog.Debug($"\t\tGetPrevious(string) : PASSAGE");
                var words = ConfigurationText[p].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length >= 2)
                {
                    if (words[researchNumber] == researchWord)
                    {
                        x = words[researchNumber + 1];
                        found = true;
                    }
                }
                p--;
            }
            //appLog.Warn($"Valeur de p : {p}");

            if (found)
                if (askPointer)
                {
                    Pointer = p;
                    return x;
                }
                else
                    return x;
            else
                return null;
        }

        /// <summary>
        /// Obtient la valeur qui suit le mot recherché en revenant en arrière sur la mémoire.
        /// </summary>
        /// <param name="researchWord">Mot recherché</param>
        /// <param name="researchNumber">Position du mot recherché</param>
        /// <param name="pStart">Position d'arrêt de la recherche</param>
        /// <returns>Donne le mot suivant à celui recherché</returns>
        private string GetPrevious(string researchWord, int researchNumber, int pStart)
        {
            var p = Pointer;
            string x = "";
            bool found = false;
            //appLog.Debug($"\tGetPrevious(string) :");
            while (p < ConfigurationText.Count && !found && p != 0 && p >= pStart)
            {
                //appLog.Debug($"\t\tGetPrevious(string) : PASSAGE");
                var words = ConfigurationText[p].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length >= 2)
                {
                    if (words[researchNumber] == researchWord)
                    {
                        x = words[researchNumber + 1];
                        found = true;
                    }
                }
                p--;
            }
            //appLog.Warn($"Valeur de p : {p}");

            if (found)
                return x;
            else
                return null;
        }
        #endregion

        #region GetNextClasse
        /// <summary>
        /// Obtient les informations du prochain utilisateur.
        /// </summary>
        /// <returns>Donne l'utilisateur</returns>
        private User GetNextUser()
        {
            var newUser = new User();

            newUser.Key = GetNext(new string[] { "AuthUserCert", "AuthPassword" }, 1);

            newUser.Name = GetNext("RealName", 1);

            newUser.UserName = GetPrevious("declare", 0);

            if (newUser.UserName != null)
                return newUser;
            else
                return null;
        }

        /// <summary>
        /// Obtient les informations de la prochaine scierie avec les utilisateurs de la scierie.
        /// </summary>
        /// <returns>Donne la scierie</returns>
        private SawMill GetNextSawMill()
        {
            var newSawMill = new SawMill();
            User newUser;

            GetNext("RadiusConvertAllMsChapv2AuthRequestToEap", 1);

            newSawMill.SawMillName = GetPrevious("declare", 0);

            while ((newUser = GetNextSawMillUsers()) != null)
            {
                if (!newSawMill.Users.ContainsKey(newUser.UserName))
                {
                    newSawMill.Users[newUser.UserName] = newUser;
                }
            }

            if (newSawMill.SawMillName != null)
                return newSawMill;
            else
                return null;
        }

        /// <summary>
        /// Obtient les informations des utilisateurs de la scierie (Utilisable avec GetNextSawMill)
        /// </summary>
        /// <returns>Donne les utilisateurs</returns>
        private User GetNextSawMillUsers()
        {

            var newUser = new User();

            var pStart = Pointer;

            GetNext("RadiusConvertAllMsChapv2AuthRequestToEap", 1);
            GetPrevious("declare", 0, true);

            var pEnd = Pointer;

            newUser.Key = GetNext(new string[] { "AuthUserCert", "AuthPassword" }, 1, pStart, pEnd);

            newUser.Name = GetNext("RealName", 1, pStart, pEnd);

            GetPrevious(new string[] { "AuthUserCert", "AuthPassword" }, 1, true);

            newUser.UserName = GetPrevious("declare", 0, pStart);

            GetNext("RealName", 1, pStart, pEnd);


            if (newUser.UserName != null)
                return newUser;
            else
                return null;
        }
        #endregion

        #region Remove()
        /// <summary>
        /// Retire de la mémoire le bloc utilisateur dans la scierie demandé.
        /// </summary>
        /// <param name="currentName">Identifiant de la scierie</param>
        /// <param name="userName">Identifiant de l'utilisateur</param>
        public void RemoveTextUser(string currentName, string userName)
        {
            SeekStarting();
            GetLine(currentName, 1);
            GetLine(userName, 1);
            var pStart = Pointer;
            Pointer += 1;
            GetLine("SendTraffic", 1);
            Pointer += 1;
            GetLine("UnicastCount", 1);
            var pEnd = Pointer + 4;

            var nb = pEnd - pStart;

            ConfigurationText.RemoveRange(pStart, nb);

            return;
        }

        /// <summary>
        /// Retire de la mémoire le bloc scierie demandé.
        /// </summary>
        /// <param name="currentName">Identifiant de la scierie</param>
        public void RemoveTextSawmill(string currentName)
        {
            SeekStarting();
            GetLine(currentName, 1);
            var pStart = Pointer;
            Pointer += 1;
            GetLine("RadiusConvertAllMsChapv2AuthRequestToEap", 1);
            Pointer += 1;
            GetLine("RadiusConvertAllMsChapv2AuthRequestToEap", 1);
            Pointer += 1;
            GetPrevious("declare", 0, true);
            var pEnd = Pointer -1;

            var nb = pEnd - pStart;

            ConfigurationText.RemoveRange(pStart, nb);

            return;
        }
        #endregion
    }
}