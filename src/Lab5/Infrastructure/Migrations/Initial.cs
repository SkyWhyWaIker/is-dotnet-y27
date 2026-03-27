using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Migrations;

[Migration(0, "Initial Migration")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        $"""
         create type operation_type as enum 
         (
            'get_user_balance',
            'user_withdraw',
            'user_deposit',
            'get_user_operation_history',
            'get_operation_history',
            'admin_check_password',
            'login',
            'logout',
            'register'
         );

         create type operation_status as enum 
         (
            'success', 
            'failure'
         ); 

         create table admins 
         (
            user_id uuid primary key not null unique,
            admin_password text not null
         );

         create table users
         (
             user_id uuid primary key not null unique,
             user_pin text not null,
             user_balance numeric not null
         );

         create table operations
         (
             user_id uuid not null,
             type operation_type not null,
             status operation_status not null,
             operation_date date not null,
             amount numeric not null
         );
         """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        $"""
             drop table operations;
             drop table users;
             drop table admins;
             drop type operation_type;
             drop type operation_status;
         """;
}