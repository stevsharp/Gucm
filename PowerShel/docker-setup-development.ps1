# Portainer - https://www.portainer.io
# URL: http://localhost:9000
docker volume create portainer_data
docker run -d -p 9000:9000 --restart always --name portainer  -v /var/run/docker.sock:/var/run/docker.sock -v portainer_data:/data portainer/portainer

# MySQL - https://www.mysql.com
# PhpMyAdmin - https://www.phpmyadmin.net
# URL: http://localhost:8080
# Username: root
# Password: power123
# Note: If you see errors then run the following commands in terminal line by line
docker run --restart always --name mysql -e MYSQL_ROOT_PASSWORD=power123 -e MYSQL_ROOT_HOST=% -p 3306:3306 -d mysql/mysql-server:latest
docker exec -it mysql sed -i -e 's/# default-authentication-plugin=mysql_native_password/default-authentication-plugin=mysql_native_password/g' /etc/my.cnf
docker exec -it mysql mysql -u root -ppower123 -e "ALTER USER root IDENTIFIED WITH mysql_native_password BY 'power123';"
docker stop mysql; docker start mysql
docker run --restart always --name phpmyadmin -d --link mysql:db -p 8080:80 phpmyadmin/phpmyadmin:latest

# SQL Server - https://www.microsoft.com/en-us/sql-server
docker run -d -p 1433:1433 --restart always --name sqlserver -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=power123" mcr.microsoft.com/mssql/server:2017-latest

# RabbitMQ - https://www.rabbitmq.com
# URL: http://localhost:15672
# Username: guest
# Password: guest
docker run -d -p 15672:15672 -p 5672:5672 --restart always --name rabbitmq --hostname my-rabbit rabbitmq:3-management

# maildev - https://github.com/djfarrelly/MailDev
# URL: http://localhost:1080
docker run -d -p 1080:80 -p 25:25 --restart always --name maildev djfarrelly/maildev
