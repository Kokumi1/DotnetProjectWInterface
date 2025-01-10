using System;
using FrontEnd.Datas;
using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FrontEnd;

class Program
{
    static void Main(string[] args)
    {

        

        
        

        bool continuer = true;

        if (!GestionClients.Authentification())
        {
            Console.WriteLine("Accès refusé. Fin du programme.");
            return;
        }

        Console.WriteLine("Bienvenue dans l'application de gestion bancaire!");

        GestionClients.InitialiserDonnees();

        GestionClients.MenuPrincipal();


    }
}