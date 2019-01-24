using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Import
{
    public static class FileImporter
    {
        /// <summary>
        /// Reads in User from given filename and returns a tuple of a User Object with FirstName and LastName
        /// Stated in header file and a list of gifts populated with titles and importance. Returns (null, null) if file not found or file
        /// incorrectly formatted.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>

        public static (User user, List<Gift> gifts) ReadInUser(string filename)
        {
            string[] lines;

            try
            {
                var path = Path.Combine(System.IO.Path.GetTempPath(), filename);

                if (File.Exists(path)) // account for relative path
                {
                    lines = System.IO.File.ReadAllLines(path);
                }

                else // account for absolute path
                {
                    lines = System.IO.File.ReadAllLines(filename);
                }
            }

            catch (Exception)
            {
                return (null, null);
            }

            (string firstname, string lastname) = ExtractNamesFromHeader(lines[0]);

            if (firstname == null || lastname == null)
            {
                return (null, null);
            }

            User user = new User();
            user.FirstName = firstname;
            user.LastName = lastname;

            List<Gift> gifts = new List<Gift>();

            for (int x = 1; x < lines.Length; x++)
            {
                Gift theGift = new Gift();
                theGift.Title = lines[x];
                theGift.Importance = x;
                gifts.Add(theGift);
            }

            return (user, gifts);
        }

        /// <summary>
        /// Takes in the correctly formatted header file and returns tuple (firstname, lastname) of header file. If header formatted
        /// incorrectly it will return tuple (null, null).
        /// </summary>
        /// <param name="header">Correctly formatted header file - 'Name: First Last' or 'Name: Last, First'</param>
        /// <returns></returns>

        public static (string firstname, string lastname) ExtractNamesFromHeader(string header)
        {
            if (!FileHeaderIsFormattedCorrectly(header))
            {
                return (null, null);
            }

            string firstName;
            string lastName;

            var fullName = header.Substring(6); //Cutting off "Name: "


            var names = fullName.Split(" ");
            if (fullName.Contains(","))
            {
                lastName = names[0].Substring(0, names[0].Length - 1); //cut off , from end of lastname
                firstName = names[1];
            }

            else
            {
                firstName = names[0];
                lastName = names[1];
            }

            return (firstName, lastName);
        }

        /// <summary>
        /// File is defined as being formatted correctly if Header Line is formatted correctly. As of right now
        /// This library is assuming that every line in the file after the first line will contain just the title of each gift to add,
        /// So it doesn't care about the format of header past the first.
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>

        public static bool FileHeaderIsFormattedCorrectly(string header)
        {
            if (header == null)
                return false;

            if (header.StartsWith("Name: "))
            {
                if (header.Trim().Split(",").Length > 2 || header.Trim().Split(" ").Length != 3) // length will always be three if no comma and string formatted correct
                    return false;

                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
