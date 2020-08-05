using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using System.Data.SqlClient;

namespace NetSubSys
{
    [Serializable]
    abstract public class Host
    {
         //переменные, константы

        protected string _IP;
        public string IP
        {
            get
            {
                return _IP;
            }
            set
            {
                if (CorrectIP(value))
                {
                    _IP = value;
                }
                else
                {
                    _IP = "127.0.0.1";
                }
            }
        }
        protected string _hostName;
        public string hostName
        {
            get
            {
                return _hostName;
            }
            set
            {
                _hostName = value;
            }
        }
        protected string _userName;
        public string userName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        } 
        protected List<NetMessage> _recievedMessages;
        public List<NetMessage> recievedMessages
        {
            get
            {
                return this._recievedMessages;
            }
        }
        public enum IPCheckResult
        {
            OK,
            WrongIPFormat,
            FirstZeroInSegment,
            TooBigValueInSegment,
            WrongChar
        }

        //процедуры, функции

        //Проверка корректности введенного IP
        internal static bool CorrectIP(string someIP)
        {
            string allowedChars = "0123456789.";
            bool wrongCharDetected = false;

            //если для какого-нибудь символа из строки someIP окажется,
            //что такого символа нет в строке allowedChars, 
            //функция вернет значение false
            foreach (char curChar in someIP)
            {
                if (allowedChars.IndexOf(curChar) == -1)
                {
                    wrongCharDetected = true;
                }
            }
            if (wrongCharDetected)
            {
                return false;
            }
            //Если в строке someIP не было обнаружено недопустимых символов,
            //будет выполнена проверка на кол-во частей IP-адреса. Их должно быть 4.
            //Если их количество оказывается неравным 4-ем, функция вернет значения false.
            else
            {
                string[] IPparts = someIP.Split('.');
                if (IPparts.Length != 4)
                {
                    return false;
                }
                //В случае, если количество частей IP-адреса равно 4-ем, будет выполнена
                //проверка "первого нуля". Если в начале какой-либо части IP-адреса, состоящей
                //более чем из одной цифры, обнаружится 0, функция вернет значение false.
                else
                {
                    bool firstZero = false;
                    foreach (string curStr in IPparts)
                    {
                        if ((curStr[0] == '0') && (curStr.Length > 1))
                        {
                            firstZero = true;
                        }
                    }
                    if (firstZero)
                    {
                        return false;
                    }
                    //Если нули в начале частей IP-адреса обнаружены не были, будет выполнена
                    //проверка на кол-во цифр в каждой части IP-адреса.
                    //Если это кол-во окажется меньше 1 или больше 3, функция вернет значение false.
                    else
                    {
                        bool tooLongValue = false;
                        foreach (string curStr in IPparts)
                        {
                            if (curStr.Length > 3 || curStr.Length < 1)
                            {
                                tooLongValue = true;
                            }
                        }
                        if (tooLongValue)
                        {
                            return false;
                        }
                        //Если ни одно из пердыдущих условий не было выполнено,
                        //функция вернет значение true.
                        else
                        {
                            bool tooBigValue = false;
                            foreach (string ipPartString in IPparts)
                            {
                                if (Convert.ToInt32(ipPartString) > 255)
                                {
                                    tooBigValue = true;
                                }
                            }
                            if (tooBigValue)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }

        //Перегрузка предыдущего метода с целью заменить тип возвращаемого значения для большей наглядности
        //результатов работы метода
        internal static Host.IPCheckResult IPCheck(string someIP)
        {
            string allowedChars = "0123456789.";
            bool wrongCharDetected = false;

            //если для какого-нибудь символа из строки someIP окажется,
            //что такого символа нет в строке allowedChars, 
            //функция вернет значение false
            foreach (char curChar in someIP)
            {
                if (allowedChars.IndexOf(curChar) == -1)
                {
                    wrongCharDetected = true;
                }
            }
            if (wrongCharDetected)
            {
                return Host.IPCheckResult.WrongChar;
            }
            //Если в строке someIP не было обнаружено недопустимых символов,
            //будет выполнена проверка на кол-во частей IP-адреса. Их должно быть 4.
            //Если их количество оказывается неравным 4-ем, функция вернет значения false.
            else
            {
                string[] IPparts = someIP.Split('.');
                if (IPparts.Length != 4)
                {
                    return Host.IPCheckResult.WrongIPFormat;
                }
                //В случае, если количество частей IP-адреса равно 4-ем, будет выполнена
                //проверка "первого нуля". Если в начале какой-либо части IP-адреса, состоящей
                //более чем из одной цифры, обнаружится 0, функция вернет значение false.
                else
                {
                    bool firstZero = false;
                    foreach (string curStr in IPparts)
                    {
                        if ((curStr[0] == '0') && (curStr.Length > 1))
                        {
                            firstZero = true;
                        }
                    }
                    if (firstZero)
                    {
                        return Host.IPCheckResult.FirstZeroInSegment;
                    }
                    //Если нули в начале частей IP-адреса обнаружены не были, будет выполнена
                    //проверка на кол-во цифр в каждой части IP-адреса.
                    //Если это кол-во окажется меньше 1 или больше 3, функция вернет значение false.
                    else
                    {
                        bool tooLongValue = false;
                        foreach (string curStr in IPparts)
                        {
                            if (curStr.Length > 3 || curStr.Length < 1)
                            {
                                tooLongValue = true;
                            }
                        }
                        if (tooLongValue)
                        {
                            return Host.IPCheckResult.TooBigValueInSegment;
                        }
                        //Если ни одно из пердыдущих условий не было выполнено,
                        //функция вернет значение true.
                        else
                        {
                            bool tooBigValue = false;
                            foreach (string ipPartString in IPparts)
                            {
                                if (Convert.ToInt32(ipPartString) > 255)
                                {
                                    tooBigValue = true;
                                }
                            }
                            if (tooBigValue)
                            {
                                return Host.IPCheckResult.TooBigValueInSegment;
                            }
                            else
                            {
                                return Host.IPCheckResult.OK;
                            }
                        }
                    }
                }
            }
        }

        //конструкторы

        public Host()
        {
            try
            {
                _hostName = System.Net.Dns.GetHostName();
            }
            catch
            {
                _hostName = "N/A";
            }
            try
            {
                int i = -1;
                do
                {
                    i++;
                    _IP = System.Net.Dns.GetHostEntry(_hostName).AddressList[i].ToString();
                }
                while ((!Host.CorrectIP(System.Net.Dns.GetHostEntry(_hostName).AddressList[i].ToString())) && (i <= System.Net.Dns.GetHostEntry(_hostName).AddressList.Length));
            }
            catch
            {
                _IP = "127.0.0.1";
            }
            try
            {
                _userName = System.Environment.UserName;
            }
            catch
            {
                _userName = "N/A";
            }
            _recievedMessages = new List<NetMessage>();
        }
        
        public Host(string hIP)
        {
            try
            {
                _hostName = System.Net.Dns.GetHostName();
            }
            catch
            {
                _hostName = "N/A";
            }
            if (CorrectIP(hIP))
            {
                _IP = hIP;
            }
            else
            {
                try
                {
                    int i = -1;
                    do
                    {
                        i++;
                        _IP = System.Net.Dns.GetHostEntry(_hostName).AddressList[i].ToString();
                    }
                    while ((!Host.CorrectIP(System.Net.Dns.GetHostEntry(_hostName).AddressList[i].ToString())) && (i <= System.Net.Dns.GetHostEntry(_hostName).AddressList.Length));
                }
                catch
                {
                    _IP = "127.0.0.1";
                }
            }
            try
            {
                _userName = System.Environment.UserName;
            }
            catch
            {
                _userName = "N/A";
            }
            _recievedMessages = new List<NetMessage>();
        }
    }

    public class Server : Host
    {
        //класссы, структуры

        //Чтобы получить возможность обрабатывать входящие подключения в отдельных потоках и при этом не использовать
        //вспомогательный вложенный класс ConnectedClient, была создана структура, на основе которой создан список 
        //List<AcitveClient> activeClients
        private struct AcitveClient
        {
            public TcpClient Client; //содержит информацию о самом клиенте
            public bool isIdle; //принимает значение false, когда данные клиента начинают обрабатываться методом ReceiveClientData()
        }

        //переменные, константы

        private int _port;
        public int port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }
        private bool _stop;
        private bool _isAcitve = false;
        public bool isAcitve
        {
            get
            {
                return _isAcitve;
            }
        }
        private TcpListener _server = null;
        private string _sqlConndectionString;
        public string sqlConnectionString
        {
            get
            {
                return _sqlConndectionString;
            }
            set
            {
                _sqlConndectionString = value;
            }
        }
        private Thread startServer = null;

        //Хранит клиентов, которые осуществили процедуру подключения к серверу (Client.ConnectToServer()).
        //На самом деле подключение не поддерживается, а после обмена соответствующими сообщениями закрывается.
        //Используется в качестве проверки наличия соединения и двухсторонней передачи данных, а также для получения
        //актуальных данных с SQL сервера.
        public List<Client> connectedClients = new List<Client>();

        //Хранит подключенных в данный момент клиентов, ожидающих ответа от сервера
        private List<Server.AcitveClient> activeClients = new List<Server.AcitveClient>(); 
        
        //делегаты и события, возможно, временные, созданные с целью проведения тестов
        //функционирования сборки с использованием элементов Windows Forms
        public delegate void ClientEventHandler(Client clientData);
        public static event ClientEventHandler ClientConnected;
        public static event ClientEventHandler ClientDisconnected;
        public static event ClientEventHandler ConnectionToClientIsLost;

        public delegate void MessageWatcher(NetMessage msg);
        public static event MessageWatcher MessageRecieved;
        public static event MessageWatcher ErrorOccured;
        public static event MessageWatcher OperationIsSuccessfullyDone;
        
        //процедуры, функции

        public List<string> ConnectionToDBTest()
        {
            List<string> result = new List<string>();
            try
            {
                //создание объекта класса SQLServer
                SQLServer sqlServer = new SQLServer();
                result.Add("Экземпляр класса sqlServer успешно создан");
                
                try
                {
                    //открытие соединения с сервером SQL
                    sqlServer.OpenConnection(sqlConnectionString);
                    result.Add("Соединение с SQL сервером успешно открыто");

                    try
                    {
                        //тестовый запрос к БД
                        sqlServer.TestQuery();
                        result.Add("Тестовый запрос к базе данных успешно выполнен");
                    }
                    catch (Exception ex)
                    {
                        result.Add("Не удалось выполнить тестовый запрос к базе данных. " + ex.Message);
                    }
                    sqlServer.CloseConnection();
                }
                catch (Exception ex)
                {
                    result.Add("Не удалось открыть соединение с SQL сервером. " + ex.Message);
                }
            }
            catch(Exception ex)
            {
                result.Add("Не удалось создать экземпляр класса sqlServer. " + ex.Message);
            }
            return result;
        }

        private void Listening()
        {
            _server = new TcpListener(IPAddress.Parse(_IP), _port);
            _server.Start();
            while (!_stop)
            {
                TcpClient connectingClient = _server.AcceptTcpClient(); //в случае обнаружения входящего соединения
                Server.AcitveClient newActiveClient = new AcitveClient(); //создается новый экземпляр структуры AciveClient
                newActiveClient.Client = connectingClient; //полю Client присваивается значение подключаемого клиента (т.е. переменной connectingClient)
                newActiveClient.isIdle = true; //и полю isIdle присваивается значение true, которое свидетельствует о том, что данный клиент не обрабатывается методом ReceiveClientData()
                activeClients.Add(newActiveClient);
                Thread connectionThread = new Thread(new ThreadStart(ReceiveClientData));
                connectionThread.IsBackground = true;
                connectionThread.Start();
            }
            _server.Stop();
        }

        //метод прослушивания не конкретного IP-адреса, а всех имеющихся в наличии адресов
        private void ListeningAnyIface()
        {
            _server = new TcpListener(IPAddress.Any, _port);
            _server.Start();
            while (!_stop)
            {
                TcpClient connectingClient = _server.AcceptTcpClient(); //в случае обнаружения входящего соединения
                Server.AcitveClient newActiveClient = new AcitveClient();
                newActiveClient.Client = connectingClient;
                newActiveClient.isIdle = true;
                activeClients.Add(newActiveClient);
                Thread connectionThread = new Thread(new ThreadStart(ReceiveClientData)); //тест
                connectionThread.IsBackground = true;
                connectionThread.Start();
            }
            _server.Stop();
        }

        //метод получения и обработки клиентских данных
        private void ReceiveClientData()
        {
            try
            {
                TcpClient curActiveClient = activeClients[activeClients.Count - 1].Client;

                //получение данных от клиента и их обработка
                NetworkStream stream = curActiveClient.GetStream();
                BinaryFormatter binFormatter = new BinaryFormatter();

                NetMessage _recievedMessage = (NetMessage)binFormatter.Deserialize(stream);

                //ввиду того, что получить тот IP-адрес, который будет непосредственно задействован при передаче сообщения,
                //на стороне клиента в момент отправки пока не удается безошибочно (за это отвечает конструктор класса NetMessage),
                //операция выполняется повторно в момент получения сообщения сервером с помощью метода GetRemoteEndPoint()
                _recievedMessage.client.IP = curActiveClient.Client.RemoteEndPoint.ToString().Split(':')[0];

                //вызов события получения сообщения для передачи этой информации в "родительский" поток
                if (MessageRecieved != null)
                {
                    MessageRecieved(_recievedMessage);
                }

                //Здесь пришлось предусмотреть различные обработчики для каждого типа
                //входящих сообщений, который определяется значением поля _command класса NetMessage

                //подключение к серверу
                //в дальнейшем, возможно, команда будет изменена (переименована, изменен код), т.к.
                //логика работы системы не требует состояния подключенности клиентов к серверу.
                //В этом случае функционал этого сегмента кода будет заключаться в проверке соединения
                //с запросом необходимой обновленной информации
                if (_recievedMessage.command == NetMessage.commandType.ConnectToTheServer)
                {
                    NetMessage answer = new NetMessage(NetMessage.commandType.Message, "OK");
                    binFormatter.Serialize(stream, answer);

                    //Выполняется проверка на наличие в списке connectedClients
                    //клиента, совпадающего с тем, что пытается подключиться.
                    bool exists = false;
                    foreach (Client curConClient in connectedClients)
                    {
                        if (curConClient.clientType == _recievedMessage.client.clientType &&
                            curConClient.hostName == _recievedMessage.client.hostName &&
                            curConClient.userName == _recievedMessage.client.userName)
                        {
                            exists = true;
                        }
                    }

                    //если в списке нет клиентов, совпадающих с тем, что пытается подключиться,
                    //подключающийся клиент добавляется в список
                    if (!exists)
                    {
                        connectedClients.Add(_recievedMessage.client);

                        //вызов события добавления клиента
                        if (ClientConnected != null)
                        {
                            ClientConnected(_recievedMessage.client);
                        }
                    }

                    //закрытие подключения
                    curActiveClient.Close();
                }


                //отключение от сервера
                if (_recievedMessage.command == NetMessage.commandType.DisconnectFromTheServer)
                {
                    NetMessage answer = new NetMessage(NetMessage.commandType.Message, "OK");
                    binFormatter.Serialize(stream, answer);
                    if (connectedClients.Count > 0)
                    {
                        //предыдущий метод поиска отключающегося клиента в списке подключенных клиентов (connectedClients) с использованием
                        //цикла while был плох тем, что, во-первых, не учитывал типа подключенного клиента (а на одном IP могут работать 
                        //несколько клиентов разных типов), и, во-вторых, в случае, если искомый клиент не был найден в connectedClients,
                        //после цикла while значение переменной i все равно будет равно индексу последнего элемента в этом списке, 
                        //что приведет к ошибочному удалению клиента
                        int disconnectingClientIndex = -1;
                        for (int curClient = 0; curClient < connectedClients.Count; curClient++)
                        {
                            if ((connectedClients[curClient].IP == _recievedMessage.client.IP) && (connectedClients[curClient].clientType == _recievedMessage.client.clientType))
                            {
                                disconnectingClientIndex = curClient;
                            }
                        }
                        if (disconnectingClientIndex != -1)
                        {
                            connectedClients.RemoveAt(disconnectingClientIndex);
                            if (ClientDisconnected != null)
                            {
                                ClientDisconnected(_recievedMessage.client);
                            }
                        }
                    }
                    //закрытие подключения.
                    //Осуществляется в любом случае, вне зависимости от того, был ли "отключен" и удален из списка connectedClient клиент,
                    //запрашивающий "отключение", или не был
                    curActiveClient.Close();
                }


                //широковещательное сообщение
                //возможно, удасться использовать этот сегмент для отправки сообщения всем подключенным клиентам
                //определенного типа (например, модулям контролера) после того, как в базе данных была изменена
                //касающаяся их информация
                if (_recievedMessage.command == NetMessage.commandType.BroadcastMessage)
                {
                    foreach (Client curConClientData in connectedClients)
                    {
                        Client curTempClient = new Client();
                        NetMessage broadcastMessage = new NetMessage(NetMessage.commandType.Message);
                        broadcastMessage.text = _recievedMessage.text;
                        curTempClient.SendMessage(curConClientData.IP, 8888, broadcastMessage, false);
                    }
                    //закрытие подключения
                    curActiveClient.Close();
                }



                //Запрос клиента на предоставление ему всей интересующей его информации
                if (_recievedMessage.command == NetMessage.commandType.GetInfo)
                {
                    if (_recievedMessage.text == "GetGroups")
                    {
                        //создание объекта класса SQLServer
                        SQLServer sqlServer = new SQLServer();
                        try
                        {
                            //попытка подключиться к базе данных, используя переменную sqlconnectionstring,
                            //которая является свойством данного класса и инициализируется с помощью файла конфигурации
                            sqlServer.OpenConnection(sqlConnectionString);

                            //запрос на получение информации из базы данных,
                            //подготовка ответа,
                            //отправка ответа клиенту.
                            //Все выполняется в блоке try, т.к. в случае возникновения нештатных ситуаций, информация о проблеме 
                            //будет передана клиенту с сообщением (блок catch). В классе Client предусмотрен метод throw exception
                            //для возможности просмотра ошибки в вызывающем метод приложении.
                            string[] columns = { "*" };

                            //т.к. модуль администратора может редактировать список групп и пользователей, а также добавлять пользователей в группы
                            //или изменять группу, к которой принадлежит пользователь, он (модуль) должен получать полную информацию о существующих группах,
                            if (_recievedMessage.client.clientType == Client.ClientType.AdministratorModule)
                            {
                                _recievedMessage.dataToSend = sqlServer.GetData("Groups", columns, null);
                            }
                            //а остальные модули получают только те группы, которые администратор настроил, как отображаемые.
                            else
                            {
                                _recievedMessage.dataToSend = sqlServer.GetData("Groups", columns, "WHERE GroupVisibility = 'True'");
                            }
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "OK";
                            binFormatter.Serialize(stream, _recievedMessage);

                            sqlServer.CloseConnection();
                        }
                        catch (Exception ex)
                        {
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "Ошибка при обработке запроса GetGroups. " + ex.Message;
                            
                            if (ErrorOccured != null)
                            {
                                ErrorOccured(_recievedMessage);
                            }
                            
                            binFormatter.Serialize(stream, _recievedMessage);

                            
                        }
                        curActiveClient.Close();
                    }


                    if (_recievedMessage.text == "GetGroupName")
                    {
                        //создание объекта класса SQLServer
                        SQLServer sqlServer = new SQLServer();
                        try
                        {
                            //попытка подключиться к базе данных, используя переменную sqlconnectionstring,
                            //которая является свойством данного класса и инициализируется с помощью файла конфигурации
                            sqlServer.OpenConnection(sqlConnectionString);

                            //подготовка ответа,
                            //отправка ответа клиенту.
                            //Все выполняется в блоке try, т.к. в случае возникновения нештатных ситуаций, информация о проблеме 
                            //будет передана клиенту с сообщением (блок catch). В классе Client предусмотрен метод throw exception
                            //для возможности просмотра ошибки в вызывающем метод приложении.
                            string[] columns = { "*" };
                            string condition = "WHERE GroupID IN (Select GroupID FROM Personal_In_Groups WHERE PersID = '" + _recievedMessage.dataToSend.Rows[0]["PersID"].ToString() + "')";
                            _recievedMessage.dataToSend = sqlServer.GetData("Groups", columns, condition);
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "OK";
                            binFormatter.Serialize(stream, _recievedMessage);
                            sqlServer.CloseConnection();
                        }
                        catch (Exception ex)
                        {
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "Ошибка при обработке запроса GetGroupName. " + ex.Message;

                            if (ErrorOccured != null)
                            {
                                ErrorOccured(_recievedMessage);
                            }

                            binFormatter.Serialize(stream, _recievedMessage);
                        }
                        curActiveClient.Close();
                    }


                    if (_recievedMessage.text == "GetUsers")
                    {
                        //создание объекта класса SQLServer
                        SQLServer sqlServer = new SQLServer();
                        try
                        {
                            //попытка подключиться к базе данных, используя переменную sqlconnectionstring,
                            //которая является свойством данного класса и инициализируется с помощью файла конфигурации
                            sqlServer.OpenConnection(sqlConnectionString);

                            //подготовка ответа,
                            //отправка ответа клиенту.
                            //Все выполняется в блоке try, т.к. в случае возникновения нештатных ситуаций, информация о проблеме 
                            //будет передана клиенту с сообщением (блок catch). В классе Client предусмотрен метод throw exception
                            //для возможности просмотра ошибки в вызывающем метод приложении.
                            string[] columns = { "*" };
                            _recievedMessage.dataToSend = sqlServer.GetData("Personal", columns, "WHERE PersStatus = 'Работает'");
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "OK";
                            binFormatter.Serialize(stream, _recievedMessage);

                            sqlServer.CloseConnection();
                        }
                        catch (Exception ex)
                        {
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "Ошибка при обработке запроса GetUsers. " + ex.Message;

                            if (ErrorOccured != null)
                            {
                                ErrorOccured(_recievedMessage);
                            }

                            binFormatter.Serialize(stream, _recievedMessage);
                        }
                        curActiveClient.Close();
                    }


                    if (_recievedMessage.text == "GetCommonCategories")
                    {
                        SQLServer sqlServer = new SQLServer();
                        try
                        {
                            sqlServer.OpenConnection(this.sqlConnectionString);
                            string[] columns = { "*" };
                            _recievedMessage.dataToSend = sqlServer.GetData("CommonCategories", columns, null);
                            _recievedMessage.text = "OK";
                            _recievedMessage.command = NetMessage.commandType.Message;
                            binFormatter.Serialize(stream, _recievedMessage);
                        }
                        catch (Exception ex)
                        {
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "Ошибка при обработке запроса GetCommonCategories. " + ex.Message;

                            if (ErrorOccured != null)
                            {
                                ErrorOccured(_recievedMessage);
                            }

                            binFormatter.Serialize(stream, _recievedMessage);
                        }
                    }


                    if (_recievedMessage.text == "GetInternalCategories")
                    {
                        SQLServer sqlServer = new SQLServer();
                        try
                        {
                            sqlServer.OpenConnection(this.sqlConnectionString);
                            string[] columns = { "*" };
                            _recievedMessage.dataToSend = sqlServer.GetData("InternalCategories", columns, null);
                            _recievedMessage.text = "OK";
                            _recievedMessage.command = NetMessage.commandType.Message;
                            binFormatter.Serialize(stream, _recievedMessage);
                        }
                        catch (Exception ex)
                        {
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "Ошибка при обработке запроса GetInternalCategories. " + ex.Message;

                            if (ErrorOccured != null)
                            {
                                ErrorOccured(_recievedMessage);
                            }

                            binFormatter.Serialize(stream, _recievedMessage);
                        }
                    }


                    if (_recievedMessage.text == "GetClaimSolvingTime")
                    {
                        try
                        {
                            SQLServer sqlServer = new SQLServer();
                            sqlServer.OpenConnection(this.sqlConnectionString);
                            string[] columnsSet = { "DATEDIFF(day, ClaimReceivedDate, ClaimSolvingReceivedDate) AS 'days'", "DATEDIFF(hour, ClaimReceivedTime, ClaimSolvingReceivedTime) AS 'hours'", "DATEDIFF(minute, ClaimReceivedTime, ClaimSolvingReceivedTime) AS 'minutes'", "DATEDIFF(second, ClaimReceivedTime, ClaimSolvingReceivedTime) AS 'seconds'" };
                            string condition = "WHERE ClaimID = " + _recievedMessage.dataToSend.Rows[0]["ClaimID"].ToString();
                            _recievedMessage.dataToSend = sqlServer.GetData("Claims", columnsSet, condition);
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "OK";
                            binFormatter.Serialize(stream, _recievedMessage);
                        }
                        catch (Exception ex)
                        {
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "Ошибка при обработке запроса GetClaimSolvingTime. " + ex.Message;

                            if (ErrorOccured != null)
                            {
                                ErrorOccured(_recievedMessage);
                            }

                            binFormatter.Serialize(stream, _recievedMessage);
                        }
                    }

                    if (_recievedMessage.text == "GetColumns")
                    {
                        try
                        {
                            SQLServer sqlServer = new SQLServer();
                            sqlServer.OpenConnection(this.sqlConnectionString);
                            string[] columnsSet = { "COLUMN_NAME" };
                            string condition = "WHERE TABLE_NAME = '" + _recievedMessage.dataToSend.Rows[0][0].ToString() + "'";
                            _recievedMessage.dataToSend = sqlServer.GetData("INFORMATION_SCHEMA.COLUMNS", columnsSet, condition);
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "OK";
                            binFormatter.Serialize(stream, _recievedMessage);
                            sqlServer.CloseConnection();
                        }
                        catch (Exception ex)
                        {
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "Ошибка при обработке запроса GetColumns. " + ex.Message;

                            if (ErrorOccured != null)
                            {
                                ErrorOccured(_recievedMessage);
                            }

                            binFormatter.Serialize(stream, _recievedMessage);
                        }
                    }

                    if (_recievedMessage.text == "GetReportInfo")
                    {
                        try
                        {
                            SQLServer sqlServer = new SQLServer();
                            sqlServer.OpenConnection(this.sqlConnectionString);
                            string[] columnsSet = { "*" };

                            //переменная condition понадобится для формирования второго сообщения от сервера
                            string condition = _recievedMessage.dataToSend.Rows[0][0].ToString();

                            if (condition.Length > 0)
                            {
                                _recievedMessage.dataToSend = sqlServer.GetData("ExecutedClaimsReport", columnsSet, " WHERE " + condition);
                            }
                            else
                            {
                                _recievedMessage.dataToSend = sqlServer.GetData("ExecutedClaimsReport", columnsSet, null);
                            }
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "OK";
                            binFormatter.Serialize(stream, _recievedMessage);

                            //подготовка второго сообщения, которое уже будет отправлено с сервера, выступающего в роли "клиента".
                            //Сообщение будет содержать информацию о количестве заявок всех типов, содержащихся в запрашиваемом отчете

                            //полю text экземпляра класса NetMessage _recievedMessage присваивается значение "reportClaimsCount",
                            //т.к. с помощью значения этого поля принимающая сторона понимает, что содержится в сообщении, и как
                            //следует обрабатываться поступающую информацию
                            _recievedMessage.text = "reportClaimsCount";

                            //Т.к. запрос на получение таблицы с количеством всех типов заявок для запрашиваемого отчета
                            //представляет собой конструкцию со вложенным оператором SELECT, необходимо некоторым образом преобразовать
                            //текст условия (часть запроса после оператора WHERE), таким образом, чтобы исключить неоднозначность определения
                            //полей. 
                            //Другими словами, к названиям полей в тексте условия (_recievedMessage.dataToSend.Rows[0][0].ToString())
                            //необходимо добавить имя таблицы, в которой эти поля содержатся
                            int curChar = 0;

                            while (curChar < condition.Length)
                            {
                                if (condition[curChar] == '[')
                                {
                                    if (curChar != 0)
                                    {
                                        condition = condition.Insert(curChar, "ExecutedClaimsReport.");
                                        curChar += "ExecutedClaimsReport.".Length;
                                    }
                                    else
                                    {
                                        condition = condition.Insert(0, "ExecutedClaimsReport.");
                                        curChar += "ExecutedClaimsReport.".Length;
                                    }
                                }
                                curChar++;
                            }

                            //Ранее в ниже следующем if в качестве условия следовало "_recievedMessage.dataToSend.Rows[0][0].ToString().Length > 0".
                            //
                            //Позже, во время реализации функции фильтрации выполненных заявок с помощью указания временных периодов между наступлениями очередных событий
                            //(время выполнения, время назначения и т.д.), показалось странным наличие такого условия.
                            //
                            //Дело в том, что возвращаемое количество строк в результате запроса к базе данных может оказаться равным 0 (т.е. пустым), и в этом случае
                            //проверка такого условия приведет к возникновению исключения. Кроме того, нет никакой логики в том, чтобы проверять длинну первой ячейки 
                            //результата запроса (а к этому моменту, _recievedMessage.dataToSend уже результат запроса к базе данных), т.к. содержимое первой ячейки
                            //(первый столбец, первая строка) может оказаться каким угодно и всецело будет зависеть от самого запроса.
                            //
                            //Поэтому выражение условия было заменено с "_recievedMessage.dataToSend.Rows[0][0].ToString().Length > 0" на "condition.Length > 0"

                            if (condition.Length > 0)
                            {
                                columnsSet = new string[3] { "tbl1.CommonCatName AS 'Категория заявки'", "tbl1.CommonCatDiscription AS 'Описание категории'", "ISNULL(tbl2.qqq, 0) AS 'Количество заявок'" };
                                _recievedMessage.dataToSend = sqlServer.GetData("CommonCategories", columnsSet, "AS tbl1 LEFT OUTER JOIN (SELECT CommonCategories.CommonCatName, CommonCategories.CommonCatDiscription, COUNT(*) AS qqq FROM CommonCategories LEFT OUTER JOIN ExecutedClaimsReport ON CommonCategories.CommonCatName = executedclaimsreport.[категория заявки] WHERE (" + condition + ") GROUP BY CommonCategories.CommonCatName, CommonCategories.CommonCatDiscription) AS tbl2 ON tbl1.CommonCatName = tbl2.CommonCatName");
                            }
                            else
                            {
                                _recievedMessage.dataToSend = sqlServer.GetData("CommonCategories", columnsSet, "AS tbl1 LEFT OUTER JOIN (SELECT CommonCategories.CommonCatName, CommonCategories.CommonCatDiscription, COUNT(*) AS qqq FROM CommonCategories LEFT OUTER JOIN ExecutedClaimsReport ON CommonCategories.CommonCatName = executedclaimsreport.[категория заявки] GROUP BY CommonCategories.CommonCatName, CommonCategories.CommonCatDiscription) AS tbl2 ON tbl1.CommonCatName = tbl2.CommonCatName");
                            }

                            //создание клиента, от лица которого будет послано сообщение
                            Client client = new Client();
                            //в качестве IP адреса и порта сервера указываются IP и порт клиента, который прислал запрос
                            //на получение отчета
                            client.serverIP = _recievedMessage.client.IP;
                            client.serverPort = _recievedMessage.client.clientSideServerPort;
                            //отправка сообщения
                            client.SendMessage(_recievedMessage, false);
                        }
                        catch (Exception ex)
                        {
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "Ошибка при обработке запроса GetReportInfo. " + ex.Message;

                            if (ErrorOccured != null)
                            {
                                ErrorOccured(_recievedMessage);
                            }

                            binFormatter.Serialize(stream, _recievedMessage);
                        }
                    }

                    //запрос на получение информации из произвольной таблицы, имя которой передается в запросе от клиента
                    if (_recievedMessage.text == "GetData")
                    {
                        try
                        {
                            SQLServer sqlServer = new SQLServer();
                            sqlServer.OpenConnection(this.sqlConnectionString);
                            string[] columnsSet = { "*" };

                            //полученное от клиента условие
                            string condition = _recievedMessage.dataToSend.Rows[0][0].ToString();

                            //полученное от клиента имя таблицы
                            string tableName = _recievedMessage.dataToSend.Rows[0][1].ToString();

                            if (condition.Length > 0)
                            {
                                _recievedMessage.dataToSend = sqlServer.GetData(tableName, columnsSet, " WHERE " + condition);
                            }
                            else
                            {
                                _recievedMessage.dataToSend = sqlServer.GetData(tableName, columnsSet, null);
                            }
                            sqlServer.CloseConnection();
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "OK";
                            binFormatter.Serialize(stream, _recievedMessage);
                        }
                        catch (Exception ex)
                        {
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "Ошибка при обработке запроса GetData. " + ex.Message;

                            if (ErrorOccured != null)
                            {
                                ErrorOccured(_recievedMessage);
                            }

                            binFormatter.Serialize(stream, _recievedMessage);
                        }
                    }


                    if (_recievedMessage.text == "")
                    {
                        try
                        {
                            //создание объекта класса SQLServer
                            SQLServer sqlServer = new SQLServer();

                            //попытка подключиться к базе данных, используя переменную sqlconnectionstring,
                            //которая является свойством данного класса и инициализируется с помощью файла конфигурации
                            sqlServer.OpenConnection(sqlConnectionString);

                            //запрос на получение информации из базы данных в зависимости от типа запрашивающего информацию клиента,
                            //создание клиента для отправки сообщения (clientToSendDataBack),
                            //отправка сообщения на сервер клиента.
                            //Все выполняется в блоке try, т.к. в случае возникновения нештатных ситуаций, информация о проблеме 
                            //будет передана клиенту с сообщением (блок catch). В классе Client предусмотрен метод throw exception
                            //для возможности просмотра ошибки в вызывающем метод приложении.
                            if (_recievedMessage.client.clientType == Client.ClientType.ControllerModule)
                            {
                                try
                                {
                                    string[] columns = { "Claims.*", "Groups.GroupName AS 'ExecGroupName'", "Personal.PersLastName AS 'ExecPersName'" };
                                    _recievedMessage.dataToSend = sqlServer.GetData("Claims", columns, " LEFT OUTER JOIN Groups ON Groups.GroupID = Claims.ExecGroupID LEFT OUTER JOIN Personal ON Personal.PersID = Claims.ExecID WHERE ClaimStatus NOT LIKE 'Выполнена' AND ClaimStatus NOT LIKE '%Удалена%' AND ClaimStatus NOT LIKE '%Отменена%'");
                                    _recievedMessage.command = NetMessage.commandType.Message;
                                    _recievedMessage.text = "OK";
                                    binFormatter.Serialize(stream, _recievedMessage);
                                }
                                catch (Exception ex)
                                {
                                    _recievedMessage.command = NetMessage.commandType.Message;
                                    _recievedMessage.text = "Ошибка при попытке сформировать данные или отправить результаты запроса GetInfo без параметров для модуля контролера. " + ex.Message;

                                    if (ErrorOccured != null)
                                    {
                                        ErrorOccured(_recievedMessage);
                                    }

                                    binFormatter.Serialize(stream, _recievedMessage);
                                }
                            }

                            if (_recievedMessage.client.clientType == Client.ClientType.UserModule)
                            {
                                try
                                {
                                    string[] columns = { "*" };
                                    _recievedMessage.dataToSend = sqlServer.GetData("Claims", columns, "WHERE ClaimStatus NOT LIKE 'Выполнена' AND ClaimStatus NOT LIKE '%Удалена%' AND ClaimStatus NOT LIKE '%Отменена%' AND ClaimSenderHostIP LIKE '" + _recievedMessage.client.IP + "'");
                                    _recievedMessage.command = NetMessage.commandType.Message;
                                    _recievedMessage.text = "OK";
                                    binFormatter.Serialize(stream, _recievedMessage);
                                }
                                catch (Exception ex)
                                {
                                    _recievedMessage.command = NetMessage.commandType.Message;
                                    _recievedMessage.text = "Ошибка при попытке сформировать данные или отправить результаты запроса GetInfo без параметров для модуля заявителя. " + ex.Message;

                                    if (ErrorOccured != null)
                                    {
                                        ErrorOccured(_recievedMessage);
                                    }

                                    binFormatter.Serialize(stream, _recievedMessage);
                                }
                            }

                            if (_recievedMessage.client.clientType == Client.ClientType.ExecutorModule)
                            {
                                //присвоение переменной execPersID значения, полученного из сообщения от клиента
                                int execPersID = Convert.ToInt32(_recievedMessage.dataToSend.Rows[0]["PersID"]);
                                string[] columns = { "GroupID" };
                                //присвоение переменной execGroupID значения, полученного из запроса к базе данных с использованием данных, полученных от клиента
                                //все выполняется внутри try, т.к., например, сотрудник с execPersID может не состоять ни в одной группе. Тогда следующая строчка
                                //приведет к возникновению ошибки.
                                try
                                {
                                    int execGroupID = Convert.ToInt32(sqlServer.GetData("Personal_In_Groups", columns, "WHERE PersID = " + execPersID.ToString()).Rows[0][0]);
                                    //формирование данных для отправки клиенту
                                    columns = new string[] { "Claims.*", "Personal.PersLastName AS 'ExecPersName'" };
                                    _recievedMessage.dataToSend = sqlServer.GetData("Claims", columns, " LEFT OUTER JOIN Personal ON Personal.PersID = Claims.ExecID WHERE Claims.ClaimStatus NOT LIKE 'Выполнена' AND Claims.ClaimStatus NOT LIKE '%Удалена%' AND Claims.ClaimStatus NOT LIKE '%Отменена%' AND Claims.ExecGroupID = '" + execGroupID.ToString() + "'");
                                    _recievedMessage.command = NetMessage.commandType.Message;
                                    _recievedMessage.text = "OK";
                                    //отправка данных клиенту
                                    binFormatter.Serialize(stream, _recievedMessage);

                                    //отправка данных о членах группы
                                    NetMessage groupMembersList = new NetMessage(NetMessage.commandType.Message, "groupMembersList");
                                    columns = new string[] { "*" };
                                    groupMembersList.dataToSend = sqlServer.GetData("Personal", columns, "WHERE (PersID IN (SELECT PersID FROM Personal_In_Groups WHERE (GroupID = '" + execGroupID.ToString() + "' AND Personal.PersStatus = 'Работает')))");
                                    Client clientToSendGroupInfo = new Client();
                                    clientToSendGroupInfo.SendMessage(_recievedMessage.client.IP, _recievedMessage.client.clientSideServerPort, groupMembersList, false);
                                }
                                catch (Exception ex)
                                {
                                    _recievedMessage.command = NetMessage.commandType.Message;
                                    _recievedMessage.text = "Ошибка при попытке сформировать данные или отправить результаты запроса GetInfo без параметров для модуля исполнителя. " + ex.Message;

                                    if (ErrorOccured != null)
                                    {
                                        ErrorOccured(_recievedMessage);
                                    }

                                    binFormatter.Serialize(stream, _recievedMessage);
                                }
                            }

                            sqlServer.CloseConnection();
                        }
                        //если не удалось подключиться к базе данных, получить данные из базы данных, создать нового клиента или отправить сообщение:
                        catch (Exception ex)
                        {
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "Ошибка при обработке запроса GetInfo без параметров. " + ex.Message;

                            if (ErrorOccured != null)
                            {
                                ErrorOccured(_recievedMessage);
                            }

                            binFormatter.Serialize(stream, _recievedMessage);
                        }
                        //закрытие подключения
                        curActiveClient.Close();
                    }
                }


                //тестовый режим отправки сообщения с заявкой на модуль контролера
                //+ вполне рабочая схема
                if (_recievedMessage.command == NetMessage.commandType.AddClaim)
                {
                    bool addingSuccessful = false;
                    _recievedMessage.dataToSend.Rows[0]["ClaimSenderHostIP"] = _recievedMessage.client.IP;
                    _recievedMessage.dataToSend.Rows[0]["ClaimStatus"] = "Ожидает назначения ответственной группы";
                    //попытка добавить полученную заявку в базу данных
                    try
                    {
                        SQLServer sqlServer = new SQLServer();
                        sqlServer.OpenConnection(sqlConnectionString);
                        sqlServer.InsertClaim(_recievedMessage);
                        //и выбрать из базы данных ее же для того, чтобы передать клиенту ID вновь созданной заявки
                        string[] columns2 = { "*" };
                        _recievedMessage.dataToSend = sqlServer.GetData("Claims", columns2, "WHERE ClaimStatus LIKE 'Ожидает назначения ответственной группы' AND ClaimSenderHostIP LIKE " + "'" + _recievedMessage.client.IP + "'" + " AND TypeOfIssue LIKE '" + _recievedMessage.dataToSend.Rows[0]["TypeOfIssue"] + "'");
                        sqlServer.CloseConnection();

                        //ответ клиенту, что его заявка добавлено в базу данных
                        _recievedMessage.text = "OK";
                        _recievedMessage.command = NetMessage.commandType.Message;
                        binFormatter.Serialize(stream, _recievedMessage);

                        addingSuccessful = true;

                        if (OperationIsSuccessfullyDone != null)
                        {
                            _recievedMessage.text = "New claim has been created";
                            OperationIsSuccessfullyDone(_recievedMessage);
                        }
                    }
                    //если добавить не получилось, клиенту отправляется сообщение с текстом ошибки
                    catch (Exception ex)
                    {
                        addingSuccessful = false;

                        _recievedMessage.command = NetMessage.commandType.Message;
                        _recievedMessage.text = "Ошибка при обработке запроса AddClaim. " + ex.Message;

                        if (ErrorOccured != null)
                        {
                            ErrorOccured(_recievedMessage);
                        }

                        binFormatter.Serialize(stream, _recievedMessage);
                    }
                    //закрытие соединения с клиентом
                    curActiveClient.Close();

                    if (addingSuccessful)
                    {
                        //после того, как заявка была добавлена в базу данных, необходимо уведомить все модули контролеров,
                        //подключенные к серверу, о возникновении новой заявки и отправить им заявку уже из базы данных, 
                        //чтобы ими был получен также и ее ID для возможности дальнейшего взаимодействия
                        SQLServer sqlServer2 = new SQLServer();
                        sqlServer2.OpenConnection(sqlConnectionString);
                        string[] columns = { "*" };
                        _recievedMessage.dataToSend = sqlServer2.GetData("Claims", columns, "WHERE ClaimStatus LIKE 'Ожидает назначения ответственной группы' AND ClaimSenderHostIP LIKE '" + _recievedMessage.client.IP + "'" + " AND TypeOfIssue LIKE '" + _recievedMessage.dataToSend.Rows[0]["TypeOfIssue"] + "'");
                        sqlServer2.CloseConnection();

                        //переменная хранит модули, которым не удалось отправить сообщение
                        List<Client> badModules = new List<Client>();

                        for (int i = 0; i < connectedClients.Count; i++)
                        {
                            if (connectedClients[i].clientType == Client.ClientType.ControllerModule)
                            {
                                Client newClient = new Client();
                                newClient.serverIP = connectedClients[i].IP;
                                newClient.serverPort = connectedClients[i].clientSideServerPort;
                                _recievedMessage.command = NetMessage.commandType.Message;

                                //рассылка информации по модулям осуществляется в блоке try 
                                //на случай, если соединение с каким-либо модулем было потеряно
                                //и отправить ему данные невозможно
                                try
                                {
                                    newClient.SendMessage(_recievedMessage, false);
                                }
                                catch
                                {
                                    //если отправить сообщение очередному модулю не удалось,
                                    //клиент добавляется в список badModules для
                                    //последующего удаления
                                    badModules.Add(connectedClients[i]);
                                }
                            }
                        }

                        //если количество элементов в списке badModulesIndexes > 0,
                        //значит в процессе рассылки отправить сообщение удалось не всем модулям,
                        //и модули, соединение с которыми было потеряно, следует удалить из списка connectedClients
                        if (badModules.Count > 0)
                        {
                            foreach (Client curBadModule in badModules)
                            {
                                int curConnectedClient = 0;
                                while (curConnectedClient < connectedClients.Count)
                                {
                                    if (curBadModule == connectedClients[curConnectedClient])
                                    {
                                        connectedClients.RemoveAt(curConnectedClient);

                                        //Для каждого удаляемого модуля генерируется соответствующее событие.
                                        //Это происходит именно на этом этапе для того, чтобы на момент обработки
                                        //этого события у обработчика была обновленная информация о подключенных клиентах
                                        //connectedClients
                                        if (ConnectionToClientIsLost != null)
                                        {
                                            ConnectionToClientIsLost(curBadModule);
                                        }
                                    }
                                    curConnectedClient++;
                                }
                            }
                        }
                    }
                }


                //Отправка информации об обновленных заявках, которым была назначена ответственная группа
                if (_recievedMessage.command == NetMessage.commandType.UpdateClaim)
                {
                    //флаг успешного обновления информации в базе данных
                    bool updateIsSuccessful = false;
                    //создание объекта класса SQLServer
                    SQLServer sqlServer = new SQLServer();
                    //переменные для хранения результатов запросов к БД
                    DataTable newDataFromDB = new DataTable();
                    DataTable newDataForExecModules = new DataTable();
                    try
                    {
                        //попытка подключиться к базе данных, используя переменную sqlconnectionstring,
                        //которая является свойством данного класса и инициализируется с помощью файла конфигурации
                        sqlServer.OpenConnection(sqlConnectionString);

                        //подготовка необходимых данных для метода sqlServer.UpdateClaim()
                        List<string> colValPairs = new List<string>();
                        List<int> ids = new List<int>();
                        for (int col = 0; col < _recievedMessage.dataToSend.Columns.Count; col++)
                        {
                            if (_recievedMessage.dataToSend.Columns[col].ColumnName != "ClaimID")
                            {
                                colValPairs.Add(_recievedMessage.dataToSend.Columns[col].ColumnName + "|" + _recievedMessage.dataToSend.Rows[0][col]);
                            }
                        }

                        //Добавление даты и времени изменения состояния заявки. Состояние может изменяться при назначении ответственной группы
                        //(тогда информация должна быть получена от модуля контролера), при назначении ответственного исполнителя
                        //(в этом случае информация поступает от модуля исполнителя) или при выполнении заявки (так же от модуля исполнителя).
                        //Следовательно, при получении информации от клиента необходимо определить, какое событие наступило

                        //информация от модуля контролера поступает только в одном единственном случае - при назначении ответственной группы.
                        if (_recievedMessage.client.clientType == Client.ClientType.ControllerModule)
                        {
                            //в связи с расширением функционала модуля контролера, информация от него теперь поступает и в случае изменения заявки.
                            //Указателем на осуществление такой операции служит значение поля text пересылаемого сообщения, которое должно содержать
                            //ID редактируемой заявки
                            //Соответственно, если длинна поля text = 0, следовательно, это не изменение заявки
                            if (_recievedMessage.text.Length == 0)
                            {
                                //для реализации возможности создания заявки с пульта контролера (когда-нибудь в дальнейшем), выполняется проверка содержимого
                                //_recievedMessage на наличие в колонке с именем "ClaimStatus" фразы "Ожидает назначения ответственного исполнителя"
                                if (_recievedMessage.dataToSend.Rows[0]["ClaimStatus"].ToString() == "Ожидает назначения ответственного исполнителя")
                                {
                                    colValPairs.Add("ExecGroupOrderReceivedDate|CONVERT(VARCHAR(50), GETDATE(), 102)");
                                    colValPairs.Add("ExecGroupOrderReceivedTime|CONVERT(VARCHAR(50), GETDATE(), 8)");
                                }
                            }
                        }

                        //если информация поступает от модуля исполнителя, то возможны 3 варианта:
                        //1. Происходит назначение заявки на конкретного исполнителя
                        //3. Исполнитель приступает к выполнению заявки
                        //2. Происходит закрытие заявки
                        if (_recievedMessage.client.clientType == Client.ClientType.ExecutorModule)
                        {
                            //в связи с расширением функционала модуля исполнителя, информация от него теперь поступает и в случае изменения заявки.
                            //Указателем на осуществление такой операции служит значение поля text пересылаемого сообщения, которое должно содержать
                            //ID редактируемой заявки
                            //Соответственно, если длинна поля text = 0, следовательно, это не изменение заявки
                            if (_recievedMessage.text.Length == 0)
                            {
                                if (_recievedMessage.dataToSend.Rows[0]["ClaimStatus"].ToString() == "Ожидает выполнения")
                                {
                                    colValPairs.Add("ExecOrderReceivedDate|CONVERT(VARCHAR(50), GETDATE(), 102)");
                                    colValPairs.Add("ExecOrderReceivedTime|CONVERT(VARCHAR(50), GETDATE(), 8)");
                                }

                                if (_recievedMessage.dataToSend.Rows[0]["ClaimStatus"].ToString() == "Выполняется")
                                {
                                    colValPairs.Add("ClaimExecStartReceivedDate|CONVERT(VARCHAR(50), GETDATE(), 102)");
                                    colValPairs.Add("ClaimExecStartReceivedTime|CONVERT(VARCHAR(50), GETDATE(), 8)");
                                }

                                if (_recievedMessage.dataToSend.Rows[0]["ClaimStatus"].ToString() == "Выполнена")
                                {
                                    colValPairs.Add("ClaimExecCompleteReceivedDate|CONVERT(VARCHAR(50), GETDATE(), 102)");
                                    colValPairs.Add("ClaimExecCompleteReceivedTime|CONVERT(VARCHAR(50), GETDATE(), 8)");
                                }
                            }
                        }

                        for (int row = 0; row < _recievedMessage.dataToSend.Rows.Count; row++)
                        {
                            ids.Add(Convert.ToInt32(_recievedMessage.dataToSend.Rows[row]["ClaimID"]));
                        }

                        //отправка запроса на сервер SQL
                        sqlServer.UpdateData("Claims", colValPairs, ids, "ClaimID");

                        //Отправка сообщения клиенту, что все ОК
                        NetMessage answer = new NetMessage(NetMessage.commandType.Message, "OK");
                        binFormatter.Serialize(stream, answer);

                        //получение обновленных заявок из базы данных
                        string[] columns = { "Claims.*", "Groups.GroupName AS 'ExecGroupName'", "Personal.PersLastName AS 'ExecPersName'" }; //{ "Claims.*", "Groups.GroupName AS 'ExecGroupName'" };
                        string condition = "";
                        foreach (int curID in ids)
                        {
                            condition += "ClaimID = '" + curID.ToString() + "' OR ";
                        }
                        condition = " LEFT OUTER JOIN Groups ON Groups.GroupID = Claims.ExecGroupID LEFT OUTER JOIN Personal ON Personal.PersID = Claims.ExecID WHERE " + condition; //" LEFT OUTER JOIN Groups ON Groups.GroupID = Claims.ExecGroupID WHERE "
                        condition = condition.Substring(0, condition.Length - 4);
                        newDataFromDB = sqlServer.GetData("Claims", columns, condition);

                        //в связи с тем, что заявки могут быть переназначены с одной группы на другую, возникает ситуация, когда в результате 
                        //выполнения команды клиента UpdateClaim кол-во заявок какой-либо группы может уменьшиться. Другими словами
                        //если с какой-то группы заявка была снята и переназначена на другую группу, это заявку нужно удалить из списка активных заявок
                        //этой группы. При этом возникает сложность, связанная с тем, что обновленные таким образом заявки будут 
                        //отправлены новому исполнителю, а старый исполнитель никакой информации о том, что с него была снята заявка, не получит.
                        //Поэтому в качестве пробного варианта при обновлении заявок осуществляется SQL запрос на получение ВСЕХ активных заявок и 
                        //их отправка "заинтересованным" клиентам, которые в свою очередь выберут из полученного набора только те, что имеют к ним отношение.
                        //Модули же исполнителей всякий раз при обновлении заявок (при получении информации с сервера) будут очищать ListView
                        //и переменные groupActiveClaims, чтобы избавиться от неактуальных более заявок.
                        condition = " LEFT OUTER JOIN Groups ON Groups.GroupID = Claims.ExecGroupID LEFT OUTER JOIN Personal ON Personal.PersID = Claims.ExecID WHERE ClaimStatus NOT LIKE 'Выполнена' AND ClaimStatus NOT LIKE '%Удалена%' AND ClaimStatus NOT LIKE '%Отменена%'";
                        newDataForExecModules = sqlServer.GetData("Claims", columns, condition);

                        //Закрытие соединения
                        sqlServer.CloseConnection();
                        updateIsSuccessful = true;

                        if (OperationIsSuccessfullyDone != null)
                        {
                            NetMessage notificationMessage = _recievedMessage;
                            notificationMessage.command = NetMessage.commandType.Message;
                            notificationMessage.text = "Claim's data has been changed for claim with ID = " + ids[0].ToString();
                            OperationIsSuccessfullyDone(notificationMessage);
                        }
                    }
                    catch (Exception ex)
                    {
                        _recievedMessage.command = NetMessage.commandType.Message;
                        _recievedMessage.text = "Ошибка при обработке запроса UpdateClaim. " + ex.Message;                        
                        
                        if (ErrorOccured != null)
                        {
                            ErrorOccured(_recievedMessage);
                        } 
                        
                        binFormatter.Serialize(stream, _recievedMessage);

                        updateIsSuccessful = false;
                    }
                    curActiveClient.Close();

                    //рассылка обновленной информации
                    if (updateIsSuccessful)
                    {
                        //переменная хранит модули, которым не удалось отправить сообщение
                        List<Client> badModules = new List<Client>();

                        foreach (Client currentConnectedClient in connectedClients)
                        {
                            //по модулям контролеров
                            if (currentConnectedClient.clientType == Client.ClientType.ControllerModule)
                            {
                                Client newClient = new Client();
                                NetMessage messToSend = new NetMessage(NetMessage.commandType.Message);
                                messToSend.dataToSend = newDataFromDB;

                                //рассылка информации по модулям осуществляется в блоке try 
                                //на случай, если соединение с каким-либо модулем было потеряно
                                //и отправить ему данные невозможно
                                try
                                {
                                    newClient.SendMessage(currentConnectedClient.IP, currentConnectedClient.clientSideServerPort, messToSend, false);
                                }
                                catch
                                {
                                    //если отправить сообщение очередному модулю не удалось,
                                    //клиент добавляется в список badModules для
                                    //последующего удаления
                                    badModules.Add(currentConnectedClient);
                                }
                            }

                            //по модулям пользователей
                            if (currentConnectedClient.clientType == Client.ClientType.UserModule)
                            {
                                //формирование DataTable с использованием тех данных, которые касаются данного пользователя
                                DataTable newDBDataToUser = new DataTable();
                                for (int colCounter = 0; colCounter < newDataFromDB.Columns.Count; colCounter++)
                                {
                                    newDBDataToUser.Columns.Add(newDataFromDB.Columns[colCounter].ColumnName);
                                }
                                for (int rowCounter = 0; rowCounter < newDataFromDB.Rows.Count; rowCounter++)
                                {
                                    if ((newDataFromDB.Rows[rowCounter]["ClaimSenderHostIP"].ToString() == currentConnectedClient.IP) &&
                                        (newDataFromDB.Rows[rowCounter]["ClaimSenderUserName"].ToString() == currentConnectedClient.userName))
                                    {
                                        DataRow newRow = newDBDataToUser.NewRow();
                                        for (int colCounter = 0; colCounter < newDataFromDB.Columns.Count; colCounter++)
                                        {
                                            newRow[colCounter] = newDataFromDB.Rows[rowCounter][colCounter];
                                        }
                                        //newRow = newDataFromDB.Rows[rowCounter];
                                        newDBDataToUser.Rows.Add(newRow);
                                    }
                                }
                                //отправка информации
                                Client newClient = new Client();
                                NetMessage messToSend = new NetMessage(NetMessage.commandType.Message);
                                messToSend.dataToSend = newDBDataToUser;
                                try
                                {
                                    newClient.SendMessage(currentConnectedClient.IP, currentConnectedClient.clientSideServerPort, messToSend, false);
                                }
                                catch
                                {
                                    badModules.Add(currentConnectedClient);
                                }
                            }

                            //по модулям исполнителей.
                            //Вместо обработки newDataFromDB обрабатывается newDataForExecModules, которая содержит информация обо всех активных заявках.
                            //Модули исполнителей при этом всякий раз при получении данных от сервера должны очищать ListView'ы и наполнять их данными заново.
                            if (currentConnectedClient.clientType == Client.ClientType.ExecutorModule)
                            {
                                //формирование DataTable с использованием тех данных, которые касаются данного пользователя
                                DataTable newDBDataToExecutor = new DataTable();
                                for (int colCounter = 0; colCounter < newDataForExecModules.Columns.Count; colCounter++)
                                {
                                    newDBDataToExecutor.Columns.Add(newDataForExecModules.Columns[colCounter].ColumnName);
                                }

                                //специально для того, чтобы можно было отправить информацию для ExecutorModule,
                                //в классе Client было добавлено свойство int dbUserID, которое необходимо для определения
                                //той информации, которая должна быть отправлена конкретному ExecutorModule

                                //получение ID группы, к которой принадлежит активный пользователь модуля исполнителя
                                sqlServer.OpenConnection(sqlConnectionString);
                                string[] columns = { "GroupID" };
                                int groupID = Convert.ToInt32(sqlServer.GetData("Personal_In_Groups", columns, "WHERE PersID = '" + currentConnectedClient.dbUserID + "'").Rows[0][0]);
                                //заполнение данными переменной newDBDataToExecutor
                                for (int row = 0; row < newDataForExecModules.Rows.Count; row++)
                                {
                                    //Т.к. теперь модулям исполнителей отправляются все невыполненные заявки, среди них могут оказаться заявки, для которых еще не назначена ответственная группа.
                                    //В этом случае, при сравнении ID группы текущего модуля исполнителя с ID ответственной группы в текущей заявке возникнет ошибка, т.к. ID группы в текущей заявке
                                    //(поле ExecGroupID) в данном случае будет равно DBNull. Поэтому необходимо выполнить дополнительную проверку
                                    if (newDataForExecModules.Rows[row]["ExecGroupID"] != DBNull.Value)
                                    {
                                        if (Convert.ToInt32(newDataForExecModules.Rows[row]["ExecGroupID"]) == groupID)
                                        {
                                            DataRow newRow = newDBDataToExecutor.NewRow();
                                            for (int col = 0; col < newDataForExecModules.Columns.Count; col++)
                                            {
                                                newRow[col] = newDataForExecModules.Rows[row][col];
                                            }
                                            newDBDataToExecutor.Rows.Add(newRow);
                                        }
                                    }
                                }
                                //отправка информации
                                Client newClient = new Client();
                                NetMessage messToSend = new NetMessage(NetMessage.commandType.Message);
                                messToSend.dataToSend = newDBDataToExecutor;
                                try
                                {
                                    newClient.SendMessage(currentConnectedClient.IP, currentConnectedClient.clientSideServerPort, messToSend, false);
                                }
                                catch
                                {
                                    badModules.Add(currentConnectedClient);
                                }
                            }
                        }

                        //если количество элементов в списке badModulesIndexes > 0,
                        //значит в процессе рассылки отправить сообщение удалось не всем модулям,
                        //и модули, соединение с которыми было потеряно, следует удалить из списка connectedClients
                        if (badModules.Count > 0)
                        {
                            foreach (Client curBadModule in badModules)
                            {
                                int curConnectedClient = 0;
                                while (curConnectedClient < connectedClients.Count)
                                {
                                    if (curBadModule == connectedClients[curConnectedClient])
                                    {
                                        connectedClients.RemoveAt(curConnectedClient);

                                        //Для каждого удаляемого модуля генерируется соответствующее событие.
                                        //Это происходит именно на этом этапе для того, чтобы на момент обработки
                                        //этого события у обработчика была обновленная информация о подключенных клиентах
                                        //connectedClients
                                        if (ConnectionToClientIsLost != null)
                                        {
                                            ConnectionToClientIsLost(curBadModule);
                                        }
                                    }
                                    curConnectedClient++;
                                }
                            }
                        }
                        //На всякий случай
                        badModules.Clear();
                    }
                    sqlServer.CloseConnection();
                }

                if (_recievedMessage.command == NetMessage.commandType.AddUser)
                {
                    SQLServer sqlServer = new SQLServer();
                    try
                    {
                        sqlServer.OpenConnection(sqlConnectionString);
                        sqlServer.InsertUser(_recievedMessage);
                        _recievedMessage.command = NetMessage.commandType.Message;
                        _recievedMessage.text = "OK";
                        binFormatter.Serialize(stream, _recievedMessage);
                        sqlServer.CloseConnection();

                        if (OperationIsSuccessfullyDone != null)
                        {
                            _recievedMessage.text = "New user with name = '" + _recievedMessage.dataToSend.Rows[0]["UserLastName"] + " "
                                                                                + _recievedMessage.dataToSend.Rows[0]["UserFirstName"] + " " 
                                                                                + _recievedMessage.dataToSend.Rows[0]["UserPatronymic"] + 
                                                                                "' has been created";
                            OperationIsSuccessfullyDone(_recievedMessage);
                        }
                    }
                    catch (Exception ex)
                    {
                        _recievedMessage.command = NetMessage.commandType.Message;
                        _recievedMessage.text = "Ошибка при обработке запроса AddUser. " + ex.Message;

                        if (ErrorOccured != null)
                        {
                            ErrorOccured(_recievedMessage);
                        }

                        binFormatter.Serialize(stream, _recievedMessage);
                    }
                }

                if (_recievedMessage.command == NetMessage.commandType.UpdateUser)
                {
                    SQLServer sqlServer = new SQLServer();
                    try
                    {
                        //подготовка необходимых данных для метода sqlServer.UpdateData()
                        //в качестве данных, которыми будут заменены существующие, выступает вторая строка
                        //таблицы _recievedMessage.dataToSend (с индексом = 1)
                        List<string> colValPairs = new List<string>();
                        List<int> ids = new List<int>();
                        for (int col = 0; col < _recievedMessage.dataToSend.Columns.Count; col++)
                        {
                            if (_recievedMessage.dataToSend.Columns[col].ColumnName != "ClaimID" &&
                                _recievedMessage.dataToSend.Columns[col].ColumnName != "ID")
                            {
                                colValPairs.Add(_recievedMessage.dataToSend.Columns[col].ColumnName + "|" + _recievedMessage.dataToSend.Rows[1][col]);
                            }
                        }

                        sqlServer.OpenConnection(sqlConnectionString);

                        //Формирование списка ID пользователей, для которых нужно обновить данные (он, разумеется, будет один)
                        List<int> idList = new List<int>();
                        idList.Add(Convert.ToInt32(_recievedMessage.dataToSend.Rows[0]["ID"].ToString()));

                        //обновление информации о пользователе
                        sqlServer.UpdateData("Personal", colValPairs, idList, "PersID");

                        //обновление информации о группе пользователя
                        string[] columns = new string[1] { "GroupID" };
                        string condition = "WHERE GroupName = '" + _recievedMessage.text + "'";
                        DataTable userGroupIDDataTable = sqlServer.GetData("Groups", columns, condition);

                        //обновление переменной colValPairs
                        colValPairs.Clear();
                        colValPairs.Add("GroupID|" + userGroupIDDataTable.Rows[0][0].ToString());

                        //сам запрос
                        sqlServer.UpdateData("Personal_In_Groups", colValPairs, idList, "PersID");

                        _recievedMessage.command = NetMessage.commandType.Message;
                        _recievedMessage.text = "OK";
                        binFormatter.Serialize(stream, _recievedMessage);
                        sqlServer.CloseConnection();

                        if (OperationIsSuccessfullyDone != null)
                        {
                            _recievedMessage.text = "User's data has been changed for user with ID = " + idList[0].ToString();
                            OperationIsSuccessfullyDone(_recievedMessage);
                        }
                    }
                    catch (Exception ex)
                    {
                        _recievedMessage.command = NetMessage.commandType.Message;
                        _recievedMessage.text = "Ошибка при обработке запроса UpdateUser. " + ex.Message;

                        if (ErrorOccured != null)
                        {
                            ErrorOccured(_recievedMessage);
                        }

                        binFormatter.Serialize(stream, _recievedMessage);
                    }
                    curActiveClient.Close();
                }

                if (_recievedMessage.command == NetMessage.commandType.AddGroup)
                {
                    SQLServer sqlServer = new SQLServer();
                    try
                    {
                        string groupName = _recievedMessage.text;
                        sqlServer.OpenConnection(sqlConnectionString);
                        sqlServer.InsertGroup(_recievedMessage);
                        _recievedMessage.command = NetMessage.commandType.Message;
                        _recievedMessage.text = "OK";
                        binFormatter.Serialize(stream, _recievedMessage);
                        sqlServer.CloseConnection();

                        if (OperationIsSuccessfullyDone != null)
                        {
                            _recievedMessage.text = "New group with name = '" + _recievedMessage.dataToSend.Rows[0]["GroupName"] + "' has been created";
                            OperationIsSuccessfullyDone(_recievedMessage);
                        }
                    }
                    catch (Exception ex)
                    {
                        _recievedMessage.command = NetMessage.commandType.Message;
                        _recievedMessage.text = "Ошибка при обработке запроса AddGroup. " + ex.Message;

                        if (ErrorOccured != null)
                        {
                            ErrorOccured(_recievedMessage);
                        }

                        binFormatter.Serialize(stream, _recievedMessage);
                    }
                }

                if (_recievedMessage.command == NetMessage.commandType.UpdateGroup)
                {
                    SQLServer sqlServer = new SQLServer();
                    try
                    {
                        //подготовка необходимых данных для метода sqlServer.UpdateData()
                        //в качестве данных, которыми будут заменены существующие, выступает вторая строка
                        //таблицы _recievedMessage.dataToSend
                        List<string> colValPairs = new List<string>();
                        List<int> ids = new List<int>();
                        for (int col = 0; col < _recievedMessage.dataToSend.Columns.Count; col++)
                        {
                            if (_recievedMessage.dataToSend.Columns[col].ColumnName != "ClaimID" &&
                                _recievedMessage.dataToSend.Columns[col].ColumnName != "ID")
                            {
                                colValPairs.Add(_recievedMessage.dataToSend.Columns[col].ColumnName + "|" + _recievedMessage.dataToSend.Rows[0][col]);
                            }
                        }

                        sqlServer.OpenConnection(sqlConnectionString);

                        //Формирование списка ID групп, для которых нужно обновить данные (он, разумеется, будет один)
                        List<int> idList = new List<int>();
                        idList.Add(Convert.ToInt32(_recievedMessage.dataToSend.Rows[0]["ID"].ToString()));

                        //обновление информации о пользователе
                        sqlServer.UpdateData("Groups", colValPairs, idList, "GroupID");

                        _recievedMessage.command = NetMessage.commandType.Message;
                        _recievedMessage.text = "OK";
                        binFormatter.Serialize(stream, _recievedMessage);
                        sqlServer.CloseConnection();

                        if (OperationIsSuccessfullyDone != null)
                        {
                            _recievedMessage.text = "Group's data has been changed for group with ID = " + idList[0].ToString();
                            OperationIsSuccessfullyDone(_recievedMessage);
                        }
                    }
                    catch (Exception ex)
                    {
                        _recievedMessage.command = NetMessage.commandType.Message;
                        _recievedMessage.text = "Ошибка при обработке запроса UpdateGroup. " + ex.Message;

                        if (ErrorOccured != null)
                        {
                            ErrorOccured(_recievedMessage);
                        }

                        binFormatter.Serialize(stream, _recievedMessage);
                    }
                    curActiveClient.Close();
                }

                if (_recievedMessage.command == NetMessage.commandType.PswdCheck)
                {
                    SQLServer sqlServer = new SQLServer();
                    try
                    {
                        sqlServer.OpenConnection(sqlConnectionString);
                        string[] columns = { "*" };
                        _recievedMessage.dataToSend = sqlServer.GetData("Personal", columns, "WHERE (PersID = " + _recievedMessage.dataToSend.Rows[0]["PersID"] + ") AND (PersPswd = HASHBYTES('SHA1','" + _recievedMessage.dataToSend.Rows[0]["PersPswd"] + "'))");
                        _recievedMessage.command = NetMessage.commandType.Message;
                        _recievedMessage.text = "OK";
                        binFormatter.Serialize(stream, _recievedMessage);
                        sqlServer.CloseConnection();
                    }
                    catch (Exception ex)
                    {
                        _recievedMessage.command = NetMessage.commandType.Message;
                        _recievedMessage.text = "Ошибка при обработке запроса PswdCheck. " + ex.Message;

                        if (ErrorOccured != null)
                        {
                            ErrorOccured(_recievedMessage);
                        }

                        binFormatter.Serialize(stream, _recievedMessage);
                    }
                    curActiveClient.Close();
                }

                if (_recievedMessage.command == NetMessage.commandType.PswdChange)
                {
                    //Создание экземпляра класса SQLServer
                    SQLServer sqlServer = new SQLServer();

                    //Если запрос на изменение пароля на самом деле является запросом на сброс пользовательского пароля,
                    //необходимо выполнить следующую последовательность действий
                    if (_recievedMessage.text == "PswdReset")
                    {
                        //создание необходимых переменных для формирования запроса данных и наполнение их данными
                        List<String> colValPairs = new List<string>();
                        colValPairs.Add("PersPswd|HASHBYTES('SHA1','" + _recievedMessage.dataToSend.Rows[0]["PersPswd"] + "')");

                        string[] columnSet = { "PersID" };

                        List<int> idList = new List<int>();

                        string condition = "WHERE PersFirstName = '" + _recievedMessage.dataToSend.Rows[0]["PersFirstName"] + "' AND PersLastName = '" +
                            _recievedMessage.dataToSend.Rows[0]["PersLastName"] + "' AND PersRole = '" + _recievedMessage.dataToSend.Rows[0]["PersRole"] + "'";

                        try
                        {
                            sqlServer.OpenConnection(sqlConnectionString);

                            //Добавление нового элемента к списку и запрос к базе данных на получение ID указанного во входящем сообщении сотрудника
                            //в одной строке
                            idList.Add(Convert.ToInt32(sqlServer.GetData("Personal", columnSet, condition).Rows[0][0]));

                            sqlServer.UpdateData("Personal", colValPairs, idList, "PersID");
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "OK";
                            binFormatter.Serialize(stream, _recievedMessage);
                            sqlServer.CloseConnection();

                            if (OperationIsSuccessfullyDone != null)
                            {
                                NetMessage notificationMessage = _recievedMessage;
                                notificationMessage.command = NetMessage.commandType.Message;
                                notificationMessage.text = "User's password has been reset for user with ID = " + idList[0].ToString();
                                OperationIsSuccessfullyDone(notificationMessage);
                            }
                        }
                        catch (Exception ex)
                        {
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "Ошибка при обработке запроса PswdReset. " + ex.Message;

                            if (ErrorOccured != null)
                            {
                                ErrorOccured(_recievedMessage);
                            }

                            binFormatter.Serialize(stream, _recievedMessage);
                        }
                    }
                    //Иначе, сброс пароля является инициируемой пользователем процедурой, и последовательность действий должна быть следующей
                    else
                    {
                        //Сперва необходимо выполнить процедуру, идентичную команде PswdCheck, чтобы убедиться, что пользователь, меняющий пароль,
                        //является тем самым пользователем, пароль для которого он меняет.
                        try
                        {
                            sqlServer.OpenConnection(sqlConnectionString);
                            string[] columns = { "*" };

                            //Если метод GetData экземпляра класса SQLServer вернул хотя бы одну строку, значит, пароль, введенный пользователем в качестве старого пароля,
                            //и пароль, хранящийся в базе данный, совпали. Следовательно, пользователю можно изменить свой пароль
                            if (sqlServer.GetData("Personal", columns, "WHERE PersFirstName = '" + _recievedMessage.dataToSend.Rows[0]["PersFirstName"] + "' AND PersLastName = '" +
                            _recievedMessage.dataToSend.Rows[0]["PersLastName"] + "' AND PersRole = '" + _recievedMessage.dataToSend.Rows[0]["PersRole"] + "' AND (PersPswd = HASHBYTES('SHA1','" + _recievedMessage.dataToSend.Rows[0]["OldPswd"] + "'))").Rows.Count == 1)
                            {
                                List<String> colValPairs = new List<string>();
                                colValPairs.Add("PersPswd|HASHBYTES('SHA1','" + _recievedMessage.dataToSend.Rows[0]["NewPswd"] + "')");

                                string[] columnSet = { "PersID" };

                                List<int> idList = new List<int>();

                                string condition = "WHERE PersFirstName = '" + _recievedMessage.dataToSend.Rows[0]["PersFirstName"] + "' AND PersLastName = '" +
                                    _recievedMessage.dataToSend.Rows[0]["PersLastName"] + "' AND PersRole = '" + _recievedMessage.dataToSend.Rows[0]["PersRole"] + "'";

                                idList.Add(Convert.ToInt32(sqlServer.GetData("Personal", columnSet, condition).Rows[0][0]));

                                sqlServer.UpdateData("Personal", colValPairs, idList, "PersID");
                                _recievedMessage.command = NetMessage.commandType.Message;
                                _recievedMessage.text = "OK";
                                binFormatter.Serialize(stream, _recievedMessage);
                                sqlServer.CloseConnection();

                                if (OperationIsSuccessfullyDone != null)
                                {
                                    NetMessage notificationMessage = _recievedMessage;
                                    notificationMessage.command = NetMessage.commandType.Message;
                                    notificationMessage.text = "User's password has been changed for user with ID = " + idList[0].ToString();
                                    OperationIsSuccessfullyDone(notificationMessage);
                                }
                            }
                            //Иначе пользователю отправляется сообщение с информацией о возникшей ошибке
                            else
                            {
                                _recievedMessage.text = "Пароль указан неверно";
                                _recievedMessage.command = NetMessage.commandType.Message;
                                binFormatter.Serialize(stream, _recievedMessage);
                                sqlServer.CloseConnection();
                            }
                        }
                        catch (Exception ex)
                        {
                            _recievedMessage.command = NetMessage.commandType.Message;
                            _recievedMessage.text = "Ошибка при обработке запроса PswdChange. " + ex.Message;

                            if (ErrorOccured != null)
                            {
                                ErrorOccured(_recievedMessage);
                            }

                            binFormatter.Serialize(stream, _recievedMessage);
                        }
                    }
                    curActiveClient.Close();
                }

                if (_recievedMessage.command == NetMessage.commandType.DeleteClaim)
                {
                    //флаг успешного обновления информации в базе данных
                    bool updateIsSuccessful = false;
                    //создание объекта класса SQLServer
                    SQLServer sqlServer = new SQLServer();
                    //переменные для хранения результатов запросов к БД
                    DataTable newDataFromDB = new DataTable();
                    DataTable newDataForExecModules = new DataTable();
                    List<int> ids = new List<int>();
                    try
                    {
                        //попытка подключиться к базе данных, используя переменную sqlconnectionstring,
                        //которая является свойством данного класса и инициализируется с помощью файла конфигурации
                        sqlServer.OpenConnection(sqlConnectionString);

                        //отправка запроса на сервер SQL
                        if (_recievedMessage.text == "deleteFromDB")
                        {
                            sqlServer.DeleteClaim(_recievedMessage, true);
                        }
                        else
                        {
                            sqlServer.DeleteClaim(_recievedMessage, false);
                        }

                        //Отправка сообщения клиенту, что все ОК
                        NetMessage answer = new NetMessage(NetMessage.commandType.Message, "OK");
                        binFormatter.Serialize(stream, answer);

                        //получение обновленных заявок из базы данных
                        string[] columns = { "Claims.*", "Groups.GroupName AS 'ExecGroupName'", "Personal.PersLastName AS 'ExecPersName'" }; //{ "Claims.*", "Groups.GroupName AS 'ExecGroupName'" };
                        string condition = "";

                        for (int i = 0; i < _recievedMessage.dataToSend.Rows.Count; i++)
                        {
                            ids.Add(Convert.ToInt32(_recievedMessage.dataToSend.Rows[i][0]));
                        }

                        foreach (int curID in ids)
                        {
                            condition += "ClaimID = '" + curID.ToString() + "' OR ";
                        }
                        condition = " LEFT OUTER JOIN Groups ON Groups.GroupID = Claims.ExecGroupID LEFT OUTER JOIN Personal ON Personal.PersID = Claims.ExecID WHERE " + condition; //" LEFT OUTER JOIN Groups ON Groups.GroupID = Claims.ExecGroupID WHERE "
                        condition = condition.Substring(0, condition.Length - 4);
                        newDataFromDB = sqlServer.GetData("Claims", columns, condition);

                        //в связи с тем, что заявки могут быть переназначены с одной группы на другую, возникает ситуация, когда в результате 
                        //выполнения команды клиента UpdateClaim кол-во заявок какой-либо группы может уменьшиться. Другими словами
                        //если с какой-то группы заявка была снята и переназначена на другую группу, это заявку нужно удалить из списка активных заявок
                        //этой группы. При этом возникает сложность, связанная с тем, что обновленные таким образом заявки будут 
                        //отправлены новому исполнителю, а старый исполнитель никакой информации о том, что с него была снята заявка, не получит.
                        //Поэтому в качестве пробного варианта при обновлении заявок осуществляется SQL запрос на получение ВСЕХ активных заявок и 
                        //их отправка "заинтересованным" клиентам, которые в свою очередь выберут из полученного набора только те, что имеют к ним отношение.
                        //Модули же исполнителей всякий раз при обновлении заявок (при получении информации с сервера) будут очищать ListView
                        //и переменные groupActiveClaims, чтобы избавиться от неактуальных более заявок.
                        condition = " LEFT OUTER JOIN Groups ON Groups.GroupID = Claims.ExecGroupID LEFT OUTER JOIN Personal ON Personal.PersID = Claims.ExecID WHERE ClaimStatus NOT LIKE 'Выполнена' AND ClaimStatus NOT LIKE '%Удалена%' AND ClaimStatus NOT LIKE '%Отменена%'";
                        newDataForExecModules = sqlServer.GetData("Claims", columns, condition);

                        //Закрытие соединения
                        sqlServer.CloseConnection();
                        updateIsSuccessful = true;

                        if (OperationIsSuccessfullyDone != null)
                        {
                            //подготовка строки с ID удаленных заявок (их может быть несколько)
                            string stringWithIds = "";
                            foreach (int curID in ids)
                            {
                                stringWithIds += curID.ToString() + ", ";
                            }
                            stringWithIds = stringWithIds.Substring(0, stringWithIds.Length - 2);

                            NetMessage notificationMessage = _recievedMessage;
                            notificationMessage.command = NetMessage.commandType.Message;
                            if (notificationMessage.text == "deleteFromDB")
                            {
                                notificationMessage.text = "Claim(s) with ID(s) = " + stringWithIds + " has(ve) been deleted from DB";
                            }
                            else
                            {
                                notificationMessage.text = "Claim(s) with ID(s) = " + stringWithIds + " has(ve) been marked as deleted";
                            }
                            OperationIsSuccessfullyDone(notificationMessage);
                        }
                    }
                    catch (Exception ex)
                    {
                        updateIsSuccessful = false;
                        _recievedMessage.command = NetMessage.commandType.Message;
                        _recievedMessage.text = "Ошибка при обработке запроса DeleteClaim. " + ex.Message;

                        if (ErrorOccured != null)
                        {
                            ErrorOccured(_recievedMessage);
                        }

                        binFormatter.Serialize(stream, _recievedMessage);
                    }
                    curActiveClient.Close();

                    //Если обновление информации о заявках (или их полное удаление) прошло успешно,
                    //рассылка обновленной информации
                    if (updateIsSuccessful)
                    {
                        //необходимо, чтобы все активные модули обновили у себя эту информацию. 
                        //При этом следует учесть, что при реальном удалении заявок из БД, будут удалены и их ID, что приведет к тому, что запрос, формирующий переменную
                        //newDataFromDB, вернет пустой результат. Также необходимо предусмотреть случай, когда реально удаленные заявки находились в списках активных заявок
                        //в модулях заявителя, исполнителя и контролера. 
                        //Учитывая все вышеизложенное, самым надежным вариантом будет обновить полностью списки актуальных заявок на всех модулях кроме модуля исполнителя,
                        //т.к. там эта процедура и так выполняется.

                        //переменная хранит модули, которым не удалось отправить сообщение
                        List<Client> badModules = new List<Client>();

                        foreach (Client currentConnectedClient in connectedClients)
                        {
                            //по модулям контролеров
                            if (currentConnectedClient.clientType == Client.ClientType.ControllerModule)
                            {
                                Client newClient = new Client();
                                NetMessage messToSend = new NetMessage(NetMessage.commandType.Message);
                                messToSend.dataToSend = newDataFromDB;

                                //рассылка информации по модулям осуществляется в блоке try 
                                //на случай, если соединение с каким-либо модулем было потеряно
                                //и отправить ему данные невозможно
                                try
                                {
                                    newClient.SendMessage(currentConnectedClient.IP, currentConnectedClient.clientSideServerPort, messToSend, false);
                                }
                                catch
                                {
                                    //если отправить сообщение очередному модулю не удалось,
                                    //клиент добавляется в список badModules для
                                    //последующего удаления
                                    badModules.Add(currentConnectedClient);
                                }
                            }

                            //по модулям пользователей
                            if (currentConnectedClient.clientType == Client.ClientType.UserModule)
                            {
                                //формирование DataTable с использованием тех данных, которые касаются данного пользователя
                                DataTable newDBDataToUser = new DataTable();
                                for (int colCounter = 0; colCounter < newDataFromDB.Columns.Count; colCounter++)
                                {
                                    newDBDataToUser.Columns.Add(newDataFromDB.Columns[colCounter].ColumnName);
                                }
                                for (int rowCounter = 0; rowCounter < newDataFromDB.Rows.Count; rowCounter++)
                                {
                                    if ((newDataFromDB.Rows[rowCounter]["ClaimSenderHostIP"].ToString() == currentConnectedClient.IP) &&
                                        (newDataFromDB.Rows[rowCounter]["ClaimSenderUserName"].ToString() == currentConnectedClient.userName))
                                    {
                                        DataRow newRow = newDBDataToUser.NewRow();
                                        for (int colCounter = 0; colCounter < newDataFromDB.Columns.Count; colCounter++)
                                        {
                                            newRow[colCounter] = newDataFromDB.Rows[rowCounter][colCounter];
                                        }
                                        //newRow = newDataFromDB.Rows[rowCounter];
                                        newDBDataToUser.Rows.Add(newRow);
                                    }
                                }
                                //отправка информации
                                Client newClient = new Client();
                                NetMessage messToSend = new NetMessage(NetMessage.commandType.Message);
                                messToSend.dataToSend = newDBDataToUser;
                                try
                                {
                                    newClient.SendMessage(currentConnectedClient.IP, currentConnectedClient.clientSideServerPort, messToSend, false);
                                }
                                catch
                                {
                                    badModules.Add(currentConnectedClient);
                                }
                            }

                            //по модулям исполнителей.
                            //Вместо обработки newDataFromDB обрабатывается newDataForExecModules, которая содержит информация обо всех активных заявках.
                            //Модули исполнителей при этом всякий раз при получении данных от сервера должны очищать ListView'ы и наполнять их данными заново.
                            if (currentConnectedClient.clientType == Client.ClientType.ExecutorModule)
                            {
                                //формирование DataTable с использованием тех данных, которые касаются данного пользователя
                                DataTable newDBDataToExecutor = new DataTable();
                                for (int colCounter = 0; colCounter < newDataForExecModules.Columns.Count; colCounter++)
                                {
                                    newDBDataToExecutor.Columns.Add(newDataForExecModules.Columns[colCounter].ColumnName);
                                }

                                //специально для того, чтобы можно было отправить информацию для ExecutorModule,
                                //в классе Client было добавлено свойство int dbUserID, которое необходимо для определения
                                //той информации, которая должна быть отправлена конкретному ExecutorModule

                                //получение ID группы, к которой принадлежит активный пользователь модуля исполнителя
                                sqlServer.OpenConnection(sqlConnectionString);
                                string[] columns = { "GroupID" };
                                int groupID = Convert.ToInt32(sqlServer.GetData("Personal_In_Groups", columns, "WHERE PersID = '" + currentConnectedClient.dbUserID + "'").Rows[0][0]);
                                //заполнение данными переменной newDBDataToExecutor
                                for (int row = 0; row < newDataForExecModules.Rows.Count; row++)
                                {
                                    //Т.к. теперь модулям исполнителей отправляются все невыполненные заявки, среди них могут оказаться заявки, для которых еще не назначена ответственная группа.
                                    //В этом случае, при сравнении ID группы текущего модуля исполнителя с ID ответственной группы в текущей заявке возникнет ошибка, т.к. ID группы в текущей заявке
                                    //(поле ExecGroupID) в данном случае будет равно DBNull. Поэтому необходимо выполнить дополнительную проверку
                                    if (newDataForExecModules.Rows[row]["ExecGroupID"] != DBNull.Value)
                                    {
                                        if (Convert.ToInt32(newDataForExecModules.Rows[row]["ExecGroupID"]) == groupID)
                                        {
                                            DataRow newRow = newDBDataToExecutor.NewRow();
                                            for (int col = 0; col < newDataForExecModules.Columns.Count; col++)
                                            {
                                                newRow[col] = newDataForExecModules.Rows[row][col];
                                            }
                                            newDBDataToExecutor.Rows.Add(newRow);
                                        }
                                    }
                                }
                                //отправка информации
                                Client newClient = new Client();
                                NetMessage messToSend = new NetMessage(NetMessage.commandType.Message);
                                messToSend.dataToSend = newDBDataToExecutor;
                                try
                                {
                                    newClient.SendMessage(currentConnectedClient.IP, currentConnectedClient.clientSideServerPort, messToSend, false);
                                }
                                catch
                                {
                                    badModules.Add(currentConnectedClient);
                                }
                            }
                        }

                        //если количество элементов в списке badModulesIndexes > 0,
                        //значит в процессе рассылки отправить сообщение удалось не всем модулям,
                        //и модули, соединение с которыми было потеряно, следует удалить из списка connectedClients
                        if (badModules.Count > 0)
                        {
                            foreach (Client curBadModule in badModules)
                            {
                                int curConnectedClient = 0;
                                while (curConnectedClient < connectedClients.Count)
                                {
                                    if (curBadModule == connectedClients[curConnectedClient])
                                    {
                                        connectedClients.RemoveAt(curConnectedClient);

                                        //Для каждого удаляемого модуля генерируется соответствующее событие.
                                        //Это происходит именно на этом этапе для того, чтобы на момент обработки
                                        //этого события у обработчика была обновленная информация о подключенных клиентах
                                        //connectedClients
                                        if (ConnectionToClientIsLost != null)
                                        {
                                            ConnectionToClientIsLost(curBadModule);
                                        }
                                    }
                                    curConnectedClient++;
                                }
                            }
                        }
                        //На всякий случай
                        badModules.Clear();
                    }
                    sqlServer.CloseConnection();
                }

                //обработчик обычного текстового сообщения
                if (_recievedMessage.command == NetMessage.commandType.Message)
                {
                    curActiveClient.Close();
                }

                //удаление клиента, закрывшего подключение, из массива активных клиентов
                for (int i = 0; i < activeClients.Count; i++)
                {
                    if (activeClients[i].Client == curActiveClient)
                    {
                        activeClients.RemoveAt(i);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ErrorOccured != null)
                {
                    NetMessage errorMsg = new NetMessage(NetMessage.commandType.Message, ex.Message);
                    ErrorOccured(errorMsg);
                }
            }
        }

        public void Start(bool AnyIface)
        {
            if (!AnyIface)
            {
                startServer = new Thread(new ThreadStart(Listening));
                startServer.Start();
                startServer.IsBackground = true;
                _isAcitve = true;
            }
            else
            {
                startServer = new Thread(new ThreadStart(ListeningAnyIface));
                startServer.IsBackground = true;
                startServer.Start();
                _isAcitve = true;
            }
        }

        public void Stop()
        {
            _stop = true;
            startServer.Abort();
            _server.Stop();
            _isAcitve = false;
        }

        //конструкторы

        public Server(string servIP, int servPort):base(servIP)
        {
            _port = servPort;
        }
        public Server(int servPort) : base()
        {
            _port = servPort;
        }
    }

    [Serializable]
    public class Client : Host
    {
        //классы

        //класс для реализации способа отправки сообщений в отдельном потоке
        protected class MessageSender
        {
            //переменные, константы

            public NetMessage msgToSend;
            public string servIP;
            public int servPort;
            public delegate void ConnectionCheckDelegate(bool status, string sIP, int sPort);
            public event ConnectionCheckDelegate ConnectionChecked;

            //процедуры, функции

            public void SendMessage()
            {
                TcpClient client = new TcpClient();
                client.Connect(servIP, servPort);
                NetworkStream stream = client.GetStream();
                BinaryFormatter binFormatter = new BinaryFormatter();
                binFormatter.Serialize(stream, msgToSend);
                client.Close();
            }

            public void ConnectToServer()
            {
                TcpClient client = new TcpClient();
                client.Connect(servIP, servPort);
                NetworkStream ns = client.GetStream();
                NetMessage regMsg = new NetMessage(NetMessage.commandType.ConnectToTheServer);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ns, regMsg);
                NetMessage answer = (NetMessage)bf.Deserialize(ns);
                if (answer.text == "OK")
                {
                    ConnectionChecked(true, servIP,servPort);
                }
                else
                {
                    ConnectionChecked(false,"",0);
                }
                client.Close();
            }

            //конструкторы

            public MessageSender(string sIP, int sPort, NetMessage msg)
            {
                servIP = sIP;
                servPort = sPort;
                msgToSend = msg;
            }
        }

        public enum ClientType
        {
            SimpleClient,
            UserModule,
            ControllerModule,
            ExecutorModule,
            AdministratorModule,
            ServerModule
        }

        //переменные, константы

        private ClientType _clientType;
        public ClientType clientType
        {
            get
            {
                return _clientType;
            }
        }
        private bool _connectedToServer;
        public bool connectedToServer
        {
            get
            {
                return _connectedToServer; 
            }
        }
        private string _serverIP;
        public string serverIP
        {
            get
            {
                return _serverIP;
            }
            set
            {
                if (Host.CorrectIP(value))
                {
                    _serverIP = value;
                }
            }
        }
        private int _serverPort;
        public int serverPort
        {
            get
            {
                return _serverPort;
            }
            set
            {
                _serverPort = value;
            }
        }
        private int _clientSideServerPort;
        public int clientSideServerPort
        {
            get
            {
                return _clientSideServerPort;
            }
            set
            {
                _clientSideServerPort = value;
            }
        }
        private int _dbUserID;
        public int dbUserID
        {
            get
            {
                return _dbUserID;
            }
            set
            {
                _dbUserID = value;
            }
        }
        public delegate void AnswerReciever(NetMessage answer);
        public static event AnswerReciever AnswerRecievedFromServer;

        //процедуры, функции

        //отправка произвольного сообщения на сервер
        public void SendMessage(string remoteIP, int remotePort, NetMessage messageToSend, bool anotherThread)
        {
            if (!anotherThread)
            {
                messageToSend.client = this;
                TcpClient client = new TcpClient();
                client.Connect(remoteIP, remotePort);
                NetworkStream stream = client.GetStream();
                BinaryFormatter binFormatter = new BinaryFormatter();
                binFormatter.Serialize(stream, messageToSend);
                if (messageToSend.command == NetMessage.commandType.AddClaim ||
                    messageToSend.command == NetMessage.commandType.GetInfo ||
                    messageToSend.command == NetMessage.commandType.UpdateClaim ||
                    messageToSend.command == NetMessage.commandType.PswdCheck ||
                    messageToSend.command == NetMessage.commandType.AddUser ||
                    messageToSend.command == NetMessage.commandType.UpdateUser ||
                    messageToSend.command == NetMessage.commandType.AddGroup ||
                    messageToSend.command == NetMessage.commandType.UpdateGroup ||
                    messageToSend.command == NetMessage.commandType.DeleteGroup ||
                    messageToSend.command == NetMessage.commandType.DeleteClaim ||
                    messageToSend.command == NetMessage.commandType.PswdChange)
                {
                    NetMessage answer = (NetMessage)binFormatter.Deserialize(stream);
                    //если ответ не ОК, значит данные были переданы серверу, и от него был получен ответ,
                    //значит, проблем с соединением между клиентом и сервером, скорее всего, нет, но что-то 
                    //еще пошло не так. Например, сервер не смог добавить информацию о заявке в базу данных.
                    //Для того, чтобы уведомить об этом пользователя, вызывается метод throw exception.
                    if (answer.text != "OK")
                    {
                        Exception myNewEx = new Exception(answer.text);
                        throw myNewEx;
                    }
                    if (AnswerRecievedFromServer != null)
                    {
                        AnswerRecievedFromServer(answer);
                    }
                }
                client.Close();
            }
            else
            {
                MessageSender ms = new MessageSender(remoteIP, remotePort, messageToSend);
                Thread sendingThread = new Thread(new ThreadStart(ms.SendMessage));
                sendingThread.Start();
                sendingThread.IsBackground = true;
            }
        }

        //перегруженный вариант предыдущего метода. Используется в случае установления соединения с  сервером
        //(метод ConnectToServer). Предотвращает неверное указание целевых IP-адреса и порта.
        //Использование данного метода без предварительной инициализации переменных _serverIP и _serverPort
        //(которая осуществляется в методе ConnectToServer) приведет к ошибке.
        public void SendMessage(NetMessage messageToSend, bool anotherThread)
        {
            if (!anotherThread)
            {
                messageToSend.client = this;
                TcpClient client = new TcpClient();
                client.Connect(this._serverIP, this._serverPort);
                NetworkStream stream = client.GetStream();
                BinaryFormatter binFormatter = new BinaryFormatter();
                binFormatter.Serialize(stream, messageToSend);
                if (messageToSend.command == NetMessage.commandType.AddClaim ||
                    messageToSend.command == NetMessage.commandType.GetInfo ||
                    messageToSend.command == NetMessage.commandType.UpdateClaim ||
                    messageToSend.command == NetMessage.commandType.PswdCheck ||
                    messageToSend.command == NetMessage.commandType.AddUser ||
                    messageToSend.command == NetMessage.commandType.UpdateUser ||
                    messageToSend.command == NetMessage.commandType.AddGroup ||
                    messageToSend.command == NetMessage.commandType.UpdateGroup ||
                    messageToSend.command == NetMessage.commandType.DeleteGroup ||
                    messageToSend.command == NetMessage.commandType.DeleteClaim ||
                    messageToSend.command == NetMessage.commandType.PswdChange)
                {
                    NetMessage answer = (NetMessage)binFormatter.Deserialize(stream);
                    //если ответ не ОК, значит данные были переданы серверу, и от него был получен ответ,
                    //значит, проблем с соединением между клиентом и сервером, скорее всего, нет, но что-то 
                    //еще пошло не так. Например, сервер не смог добавить информацию о заявке в базу данных.
                    //Для того, чтобы уведомить об этом пользователя, вызывается метод throw exception.
                    if (answer.text != "OK")
                    {
                        Exception myNewEx = new Exception(answer.text);
                        throw myNewEx;
                    }
                    if (AnswerRecievedFromServer != null)
                    {
                        AnswerRecievedFromServer(answer);
                    }
                }
                client.Close();
            }
            else
            {
                MessageSender ms = new MessageSender(this._serverIP, this._serverPort, messageToSend);
                Thread sendingThread = new Thread(new ThreadStart(ms.SendMessage));
                sendingThread.Start();
                sendingThread.IsBackground = true;
            }
        }

        //отправка на сервер специального сообщения с типом команды RegOnServer и получение ответа от сервера,
        //содержащего строку "OK". Если сообщение получено, значение переменной _connectedToServer становится 
        //равным true.
        //На самом деле подключение не поддерживается, а после обмена соответствующими сообщениями закрывается.
        //Используется в качестве проверки наличия соединения и двухсторонней передачи данных, а также для получения
        //актуальных данных с SQL сервера.
        public void ConnectToServer(string serverIP, int serverPort)
        {
            TcpClient client = new TcpClient();
            client.Connect(serverIP, serverPort);
            NetworkStream ns = client.GetStream();
            NetMessage regMsg = new NetMessage(NetMessage.commandType.ConnectToTheServer);
            regMsg.client = this;
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ns, regMsg);
            NetMessage answer = (NetMessage)bf.Deserialize(ns);
            if (answer.text == "OK")
            {
                _connectedToServer = true;
                this._serverIP = serverIP;
                this._serverPort = serverPort;
            }
            else
            {
                Exception myNewEx = new Exception(answer.text);
                throw myNewEx;
            }
            client.Close();
        }

        //метод для делегата ConnectionCheckDelegate класса MessageSender
        public void TryConnection(bool result, string strIP, int intPort)
        {
            if (result)
            {
                this._connectedToServer = true;
                this._serverIP = strIP;
                this._serverPort = intPort;
            }
            else
            {
                this._connectedToServer = false;
            }
        }

        //отправка на сервер сообщения о разрыве соединения (disconect).
        //Используется для инициации удаления хоста с клиентом, посылающим это сообщение, из таблицы базы данных
        public void DisconectFromServer()
        {
            TcpClient client = new TcpClient();
            client.Connect(_serverIP, _serverPort);
            NetworkStream ns = client.GetStream();
            NetMessage regMsg = new NetMessage(NetMessage.commandType.DisconnectFromTheServer);
            regMsg.client = this;
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ns, regMsg);
            NetMessage answer = (NetMessage)bf.Deserialize(ns);
            if (answer.text == "OK")
            {
                _connectedToServer = false;
                //this._serverIP = null;
            }
            client.Close();
        }

        //конструкторы

        public Client() : base()
        {
            _connectedToServer = false;
            _clientType = ClientType.SimpleClient;
            _dbUserID = -1;
        }
        public Client(ClientType typeOfClient) : base()
        {
            _connectedToServer = false;
            _clientType = typeOfClient;
            _dbUserID = -1;
        }
    }

    [Serializable]
    public class NetMessage
    {
        //Переменные, константы

        private Client _client;
        public Client client
        {
            get
            {
                return _client;
            }
            set
            {
                _client = value;
            }
        }
        private string _text;
        public string text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }
        public enum commandType
        {
            ConnectToTheServer, //проверка двухстороннего соединения, получение актуальных данных с SQL сервера
            DisconnectFromTheServer,
            CheckTheConnection, //переодическая(?) проверка соединения с клиентами (если выполняется на стороне сервера)
            Message, //отправка текста
            BroadcastMessage, //отправка текста всем "подключенным" клиентам
            PswdCheck,
            PswdChange,
            GetInfo,
            AddClaim,
            UpdateClaim,
            DeleteClaim,
            AddUser,
            UpdateUser,
            DeleteUser,
            AddGroup,
            UpdateGroup,
            DeleteGroup
        }
        protected commandType _command;
        public commandType command
        {
            get
            {
                return _command;
            }
            set
            {
                _command = value;
            }
        }
        public DataTable dataToSend;

        //Процедуры, функции

        //Конструкторы

        public NetMessage()
        {
            _client = new Client();
            dataToSend = new DataTable();
            _command = commandType.Message;
            _text = "";
        }
        public NetMessage(commandType typeOfCommand)
        {
            _client = new Client();
            dataToSend = new DataTable();
            _command = typeOfCommand;
            _text = "";
        }
        public NetMessage(commandType typeOfCommand, string textToSend)
        {
            _client = new Client();
            dataToSend = new DataTable();
            _text = textToSend;
            _command = typeOfCommand;
        }
    }

    public class SQLServer
    {
        //классы, структуры

        //переменные, константы

        private SqlConnection cqlCon = null;

        //процедуры, функции

        public void OpenConnection(string connectionString)
        {
            this.cqlCon = new SqlConnection();
            this.cqlCon.ConnectionString = connectionString;
            this.cqlCon.Open();
        }

        public void CloseConnection()
        {
            this.cqlCon.Close();
        }

        public bool TestQuery()
        {
            try
            {
                string sql = string.Format("SELECT * FROM sys.tables");
                SqlCommand cmd = new SqlCommand(sql, cqlCon);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataTable GetData(string tableName, string[] columnsSet, string condition)
        {
            string columns = "";
            string sql = "";
            foreach (string curColumn in columnsSet)
            {
                columns += curColumn + ", ";
            }
            columns = columns.Substring(0, columns.Length - 2);
            if (condition == null)
            {
                sql = "SELECT " + columns + " FROM " + tableName;
            }
            else
            {
                sql = "SELECT " + columns + " FROM " + tableName + " " + condition;
            }
            SqlCommand cmd = new SqlCommand(sql,cqlCon);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            return dt;
        }
        
        public void InsertClaim(NetMessage claimData)
        {
            string sql = string.Format("Insert into Claims" +
                "(ClaimSenderUserName, ClaimSenderHostName, ClaimSenderHostIP, ClaimSenderRoom, ClaimSenderName, ClaimSenderPhone, TypeOfIssue, ClaimDiscription, ClaimStatus, ClaimReceivedDate, ClaimReceivedTime)  Values" +
                "('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', CONVERT(varchar(50), GETDATE(), 102), CONVERT(varchar(50), GETDATE(), 8))", 
                claimData.dataToSend.Rows[0]["ClaimSenderUserName"], 
                claimData.dataToSend.Rows[0]["ClaimSenderHostName"], 
                claimData.dataToSend.Rows[0]["ClaimSenderHostIP"], 
                claimData.dataToSend.Rows[0]["ClaimSenderRoom"],
                claimData.dataToSend.Rows[0]["ClaimSenderName"],
                claimData.dataToSend.Rows[0]["ClaimSenderPhone"], 
                claimData.dataToSend.Rows[0]["TypeOfIssue"], 
                claimData.dataToSend.Rows[0]["ClaimDiscription"], 
                claimData.dataToSend.Rows[0]["ClaimStatus"]);

            SqlCommand cmd = new SqlCommand(sql, cqlCon);
            cmd.ExecuteNonQuery();
        }

        //для добавляемого пользователя выполняется процедура добавления пользователя в группу,
        //которая представляет собой еще один запрос к базе данных
        public void InsertUser(NetMessage dataToInsert)
        {
            string sql = string.Format("Insert into Personal" + "(PersFirstName, PersLastName, PersPatronymic, PersRole, PersStatus, PersPswd) VALUES" +
                "('{0}', '{1}', '{2}', '{3}', '{4}', HASHBYTES('SHA1','qwerty'))",
                dataToInsert.dataToSend.Rows[0]["UserFirstName"],
                dataToInsert.dataToSend.Rows[0]["UserLastName"],
                dataToInsert.dataToSend.Rows[0]["UserPatronymic"],
                dataToInsert.dataToSend.Rows[0]["UserRole"],
                dataToInsert.dataToSend.Rows[0]["UserStatus"]);

            SqlCommand cmd = new SqlCommand(sql, cqlCon);
            cmd.ExecuteNonQuery();

            sql = "INSERT INTO Personal_In_Groups (PersID, GroupID) SELECT Personal.PersID, Groups.GroupID FROM Personal CROSS JOIN Groups WHERE (Personal.PersID = (SELECT IDENT_CURRENT('Personal')) AND (Groups.GroupID = (SELECT GroupID FROM Groups AS Groups_1 WHERE (GroupName = '" + dataToInsert.dataToSend.Rows[0]["UserGroup"] + "'))))";


            cmd = new SqlCommand(sql, cqlCon);
            cmd.ExecuteNonQuery();
        }

        public void InsertGroup(NetMessage dataToInsert)
        {
            string sql = string.Format("INSERT INTO Groups" + "(GroupName, GroupComment, GroupVisibility) VALUES" + "('{0}', '{1}', '{2}')",
                dataToInsert.dataToSend.Rows[0]["GroupName"],
                dataToInsert.dataToSend.Rows[0]["GroupComment"],
                dataToInsert.dataToSend.Rows[0]["GroupVisibility"]);

            SqlCommand cmd = new SqlCommand(sql, cqlCon);
            cmd.ExecuteNonQuery();
        }

        public void UpdateData(string tableName, List<string> columsValuesPairs, List<int> IDs, string idName)
        {
            string sql = "Update " + tableName + " Set ";
            string colValstring = "";
            foreach (string curColVal in columsValuesPairs)
            {
                //если в паре имя столбца : значение встречается фраза "CONVERT(VARCHAR", значит, 
                //использовать одинарные кавычки при составлении запроса не нужно, иначе команда SQL
                //будет интерпретирована как строка
                if (curColVal.IndexOf("CONVERT(VARCHAR") != -1 || curColVal.IndexOf("HASHBYTES") != -1)
                {
                    colValstring += curColVal.Split('|')[0] + " = " + curColVal.Split('|')[1] + ", ";
                }
                else
                {
                    if (curColVal.Split('|')[1] != DBNull.Value.ToString())
                    {
                        colValstring += curColVal.Split('|')[0] + " = '" + curColVal.Split('|')[1] + "', ";
                    }
                    else
                    {
                        colValstring += curColVal.Split('|')[0] + " = NULL, ";
                    }
                }
            }
            colValstring = colValstring.Substring(0, colValstring.Length - 2);
            sql += colValstring;
            string condition = "";
            foreach (int curID in IDs)
            {
                condition += idName + " = " + "'" + curID.ToString() + "' OR "; 
            }
            condition = " WHERE " + condition.Substring(0, condition.Length - 4);
            sql += condition;
            SqlCommand sqlCommand = new SqlCommand(sql, this.cqlCon);
            sqlCommand.ExecuteNonQuery();
        }

        //второй параметр используется для того, чтобы выбрать способ удаления заявки, а их существует два. 
        //Первый способ это не фактическое удаление из бд, а изменение статуса на "удалено" или что-то подобное.
        //Второй способ это удаление записи из базы данных.
        public void DeleteClaim(NetMessage claimsData, bool deleteFromTable)
        {
            //Если запись удалять из таблицы не требуется, а необходимо лишь отметить ее как удаленную
            if (!deleteFromTable)
            {
                //формируется переменная типа List<int>, содержащая ID заявок, которые необходимо отметить как удаленные
                List<int> claimsIDs = new List<int>();
                for (int i = 0; i < claimsData.dataToSend.Rows.Count; i++)
                {
                    claimsIDs.Add(Convert.ToInt32(claimsData.dataToSend.Rows[i]["ClaimID"]));
                }

                //формируется список пар "столбец:значение", содержащих информацию о том, в каких столбцах
                //и на какие данные следует обновить таблицу
                List<string> updatedData = new List<string>();
                for (int j = 1; j < claimsData.dataToSend.Columns.Count; j++) //j от 1, потому что в 0-ом столбце ID заявок
                {
                    updatedData.Add(claimsData.dataToSend.Columns[j].ColumnName + "|" + claimsData.dataToSend.Rows[0][j].ToString()); 
                }

                //Вызывается уже написанная процедура обновления данных в таблицах бд
                UpdateData("Claims", updatedData, claimsIDs, "ClaimID");
            }
            else
            {
                //формируется строка с соответствующим SQL-запросом к БД
                string sqlQuery = "DELETE FROM Claims WHERE ";
                for (int i = 0; i < claimsData.dataToSend.Rows.Count; i++)
                {
                    sqlQuery += "ClaimID = '" + claimsData.dataToSend.Rows[i][0].ToString() + "' OR ";
                }
                //Удаление последнего OR
                sqlQuery = sqlQuery.Substring(0, sqlQuery.Length - 4);

                //Выполнение запроса
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, this.cqlCon);
                sqlCommand.ExecuteNonQuery();
            }
        }

        //конструкторы, деструкторы
    }
}
