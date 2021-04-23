@echo off
echo this script will setup the database and admin user

set /p PGPASSWORD="enter the password for the root user (postgres) of your postgres database: "

echo reseting database
psql -U postgres -f scripts\reset.sql

echo deleting existing images
rmdir /S /Q LocalStorage
mkdir LocalStorage
mkdir LocalStorage\Images
mkdir LocalStorage\Exports
mkdir LocalStorage\Exports\Images


echo creating user
psql -U postgres -f scripts\createUser.sql

echo database
set /p PGPASSWORD="enter the password for the Admin user: "
psql -U TourPlannerAdmin -d TourPlanner -f scripts\createDatabase.sql





pause