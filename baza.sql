-- phpMyAdmin SQL Dump
-- version 2.11.4
-- http://www.phpmyadmin.net
--
-- Raèunalo: localhost
-- Vrijeme generiranja: Svi 01, 2008 u 04:07 PM
-- Verzija poslužitelja: 5.0.45
-- PHP verzija: 5.2.3

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";

--
-- Baza podataka: `mobilis_pretplata`
--

-- --------------------------------------------------------

--
-- Tablièna struktura za tablicu `informacije`
--

CREATE TABLE `informacije` (
  `id` int(11) unsigned NOT NULL auto_increment,
  `zahtjev` tinyint(3) unsigned NOT NULL COMMENT 'index zahtjeva',
  `mid` int(11) unsigned NOT NULL COMMENT 'mobitel id',
  `uid` int(11) unsigned NOT NULL COMMENT 'user id',
  `dodano` datetime NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=20 ;

-- --------------------------------------------------------

--
-- Tablièna struktura za tablicu `mobiteli`
--

CREATE TABLE `mobiteli` (
  `id` int(11) unsigned NOT NULL auto_increment,
  `ime` varchar(64) NOT NULL,
  `created` datetime NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=6 ;

-- --------------------------------------------------------

--
-- Tablièna struktura za tablicu `poslovnice`
--

CREATE TABLE `poslovnice` (
  `ime` varchar(32) NOT NULL,
  `created` datetime NOT NULL,
  `info` varchar(64) NOT NULL,
  `id` int(11) unsigned NOT NULL auto_increment,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=4 ;

-- --------------------------------------------------------

--
-- Tablièna struktura za tablicu `postavke`
--

CREATE TABLE `postavke` (
  `id` int(11) unsigned NOT NULL auto_increment,
  `ime` varchar(12) NOT NULL,
  `osnovna` varchar(128) NOT NULL,
  `vrijednost` varchar(128) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=5 ;

-- --------------------------------------------------------

--
-- Tablièna struktura za tablicu `pretplate`
--

CREATE TABLE `pretplate` (
  `mobitel` varchar(11) NOT NULL,
  `created` datetime NOT NULL,
  `pocetak` date NOT NULL,
  `osnovno_trajanje` tinyint(3) unsigned NOT NULL,
  `trajanje` tinyint(3) unsigned NOT NULL,
  `produljenje_trajanje` tinyint(3) unsigned NOT NULL,
  `izmjena` datetime NOT NULL,
  `zid` int(11) unsigned NOT NULL,
  `pid` int(11) unsigned NOT NULL,
  `id` int(11) unsigned NOT NULL auto_increment,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tablièna struktura za tablicu `pretplatnici`
--

CREATE TABLE `pretplatnici` (
  `ime` varchar(32) NOT NULL,
  `prezime` varchar(64) NOT NULL,
  `ulica` varchar(64) NOT NULL,
  `grad` varchar(64) NOT NULL,
  `pb` varchar(5) NOT NULL,
  `mbg` varchar(13) NOT NULL,
  `changes` datetime NOT NULL,
  `created` datetime NOT NULL,
  `info` varchar(64) NOT NULL,
  `zid` int(11) unsigned NOT NULL,
  `id` int(11) unsigned NOT NULL auto_increment,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tablièna struktura za tablicu `zaposlenici`
--

CREATE TABLE `zaposlenici` (
  `username` varchar(32) NOT NULL,
  `password` varchar(32) NOT NULL,
  `info` varchar(64) NOT NULL,
  `lastlogin` datetime NOT NULL,
  `created` datetime NOT NULL,
  `pristup` tinyint(3) unsigned NOT NULL,
  `pid` int(11) unsigned NOT NULL,
  `id` int(11) unsigned NOT NULL auto_increment,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;
