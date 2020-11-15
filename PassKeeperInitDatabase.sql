DROP DATABASE IF EXISTS passkeeper;
CREATE DATABASE passkeeper;
USE passkeeper;

#Все пользователи ресурса.
CREATE TABLE users (
  id INT NOT NULL AUTO_INCREMENT,
  user_name VARCHAR(50) NOT NULL,
  userlast_name VARCHAR(50),
  login VARCHAR(50) NOT NULL,
  email VARCHAR(70) DEFAULT NULL,
  PRIMARY KEY (id),
  UNIQUE (login),
  UNIQUE (email)
);

#Все зарегистрированные организации.
CREATE TABLE enterprises (
  id INT NOT NULL AUTO_INCREMENT,
  enterprise_name VARCHAR(200) NOT NULL,
  enterprise_address VARCHAR(75),
  enterprise_email VARCHAR(75),
  PRIMARY KEY (id),
  UNIQUE (enterprise_email), 
  UNIQUE (enterprise_address)
);
  
#Работники организаций, один пользователь может 
#быть работником нескольких организаций и организация
#может содержать множество сотрудников. 
#Таблица является промежуточной для организации 
#связи многие ко многим.
CREATE TABLE workers (
  user_id INT NOT NULL,
  enterprise_id INT NOT NULL,
  PRIMARY KEY (user_id, enterprise_id),
  FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE,
  FOREIGN KEY (enterprise_id) REFERENCES enterprises (id) ON DELETE CASCADE
);

#Кошельки, являются хранилищем аккаунтов.
#Один аккаунт может относиться к одному кошельку,
#кошелёк может принадлежать организации или пользователю.
CREATE TABLE wallets (
  id INT NOT NULL AUTO_INCREMENT,
  wallet_name VARCHAR(50) NOT NULL,
  PRIMARY KEY (id)
);

#Аккаунты, являются содержимым кошельков.
CREATE TABLE accounts (
  id INT NOT NULL AUTO_INCREMENT,
  wallet_id INT NOT NULL,
  account_name VARCHAR(100),
  accounta_ddress VARCHAR(500),
  account_login VARCHAR(256),
  account_password VARCHAR(256),
  PRIMARY KEY (id),
  FOREIGN KEY (wallet_id) REFERENCES wallets (id) ON DELETE CASCADE ON UPDATE CASCADE
);

#Список корпоративных кошельков, таблица связывает
#кошельки с организациями.
CREATE TABLE enterprise_wallets (
  wallet_id INT NOT NULL,
  enterprise_id INT NOT NULL,
  PRIMARY KEY (wallet_id, enterprise_id),
  FOREIGN KEY (wallet_id) REFERENCES wallets (id) ON DELETE CASCADE,
  FOREIGN KEY (enterprise_id) REFERENCES enterprises (id) ON DELETE CASCADE,
  UNIQUE (wallet_id) #Не позволяет нескольким организациям влядеть одним кошельком
);

#Список администраторов корпоративных кошельков,
#связывает работников организации с кошельками организации.
#Не позволяет сделать администратором корпоративного
#кошелька пользователя, не являющегося сотрудником.
CREATE TABLE enterprise_wallets_administrators (
  user_id INT NOT NULL,
  enterprise_id INT NOT NULL,
  wallet_id INT NOT NULL,
  PRIMARY KEY (user_id, enterprise_id, wallet_id),
  FOREIGN KEY (user_id, enterprise_id) REFERENCES workers (user_id, enterprise_id) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (wallet_id, enterprise_id) REFERENCES enterprise_wallets (wallet_id, enterprise_id) ON DELETE CASCADE ON UPDATE CASCADE
);

#Сотрудники, допущенные к просмотру корпоративных
#кошельков. Такие пользователи видят содрежимое всего 
#корпоративного кошелька.
CREATE TABLE enterprise_wallets_approved_workers (
  user_id INT NOT NULL,
  enterprise_id INT NOT NULL,
  wallet_id INT NOT NULL,
  PRIMARY KEY (user_id, enterprise_id, wallet_id),
  FOREIGN KEY (user_id, enterprise_id) REFERENCES workers (user_id, enterprise_id) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (wallet_id, enterprise_id) REFERENCES enterprise_wallets (wallet_id, enterprise_id) ON DELETE CASCADE ON UPDATE CASCADE
);

#Таблица личных кошкльков, связывает
#кошельки с пользователями.
CREATE TABLE personal_wallets (
  wallet_id INT NOT NULL,
  user_id INT NOT NULL,
  PRIMARY KEY (wallet_id, user_id),
  FOREIGN KEY (wallet_id) REFERENCES wallets (id) ON DELETE CASCADE,
  FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE,
  UNIQUE (wallet_id) #Не позволяет нескольким пользователям влядеть одним кошельком
);

#Пользователи, допущенные к просмотру индивидуального кошелька.
#Такие пользователи видят весь кошелек сразу
CREATE TABLE personal_wallets_approved_users (
  wallet_id INT NOT NULL,
  user_id INT NOT NULL,
  PRIMARY KEY (wallet_id, user_id),
  FOREIGN KEY (wallet_id) REFERENCES wallets (id) ON DELETE CASCADE,
  FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE,
  UNIQUE (wallet_id, user_id) #Не позволяет дважды получить доступ к кошельку
);

#История просмотров для паролей, связывает аккаунт с пользователями
#которые его просматривали.
CREATE TABLE browsing_history (
  user_id INT NOT NULL,
  account_id INT NOT NULL,
  PRIMARY KEY (user_id, account_id),
  FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE,
  FOREIGN KEY (account_id) REFERENCES accounts (id) ON DELETE CASCADE
);