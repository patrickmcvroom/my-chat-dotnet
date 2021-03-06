﻿using System;
using System.IO;
using System.Security;
using Newtonsoft.Json;
using MindLink.Recruitment.MyChat.Data;

namespace MindLink.Recruitment.MyChat
{
    public class ConversationWriter
    {
        /// <summary>
        /// Helper method to write the <paramref name="conversation"/> as JSON to <paramref name="outputFilePath"/>.
        /// </summary>
        /// <param name="conversation">
        /// The conversation.
        /// </param>
        /// <param name="outputFilePath">
        /// The output file path.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when there is a problem with the <paramref name="outputFilePath"/>.
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when something else bad happens.
        /// </exception>
        public void WriteConversation(Conversation conversation, string outputFilePath)
        {
            try
            {
                var writer = new StreamWriter(new FileStream(outputFilePath, FileMode.Create, FileAccess.ReadWrite));

                var serialized = JsonConvert.SerializeObject(conversation, Formatting.Indented);

                writer.Write(serialized);

                writer.Flush();

                writer.Close();
            }
            catch (SecurityException securityEx)
            {
                throw new ArgumentException("You don't have permission to this file.", securityEx);
            }
            catch (DirectoryNotFoundException directoryNotFoundEx)
            {
                throw new ArgumentException("The directory path is invalid.", directoryNotFoundEx);
            }
            catch (IOException inputOutputEx)
            {
                throw new IOException("There's an error in the I/O.", inputOutputEx);
            }
        }
    }
}
