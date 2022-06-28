drop database if exists route256;
create database route256;

-- Connect to a newly-created database at this point

create schema core;

create type status as enum ('New', 'InProgress', 'Pending');

create table core.orders
(
    id            uuid               default gen_random_uuid(),
    client_id     bigint    not null,
    creation_date timestamp not null default current_timestamp,
    pickup_date   timestamp,
    status        status,
    items_data    json      not null,
    warehouse_id  bigint    not null,
    constraint pk_orders primary key (id, warehouse_id)
) partition by list (warehouse_id);

create table core.orders_wh01 partition of core.orders for values in (1);
create table core.orders_wh02 partition of core.orders for values in (2);
create table core.orders_wh03 partition of core.orders for values in (3);
create table core.orders_wh04 partition of core.orders for values in (4);
create table core.orders_wh05 partition of core.orders for values in (5);
create table core.orders_wh06 partition of core.orders for values in (6);
create table core.orders_wh07 partition of core.orders for values in (7);
create table core.orders_wh08 partition of core.orders for values in (8);
create table core.orders_wh09 partition of core.orders for values in (9);
create table core.orders_wh10 partition of core.orders for values in (10);
create table core.orders_wh11 partition of core.orders for values in (11);
create table core.orders_wh12 partition of core.orders for values in (12);
create table core.orders_wh13 partition of core.orders for values in (13);
create table core.orders_wh14 partition of core.orders for values in (14);
create table core.orders_wh15 partition of core.orders for values in (15);
create table core.orders_wh16 partition of core.orders for values in (16);
create table core.orders_wh17 partition of core.orders for values in (17);
create table core.orders_wh18 partition of core.orders for values in (18);
create table core.orders_wh19 partition of core.orders for values in (19);
create table core.orders_wh20 partition of core.orders for values in (20);
