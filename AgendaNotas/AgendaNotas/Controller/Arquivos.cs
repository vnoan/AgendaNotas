using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;
using System.Diagnostics;

namespace AgendaNotas.Controller
{
    abstract class Arquivos
    {
        public static async Task<IFile> LoadFile()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("AgendaNotasFiles", CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync("materias.txt", CreationCollisionOption.OpenIfExists);
            return file;
        }

        public static async Task WriteOnFile(IFile file, string[] materias)
        {
            //Sobrescreve o que está no file
            await file.WriteAllTextAsync(materias.ToString()); 
        }
        
        public static async Task<string[]> ReadOfFile(IFile file)
        {
            string textoFromArq = await file.ReadAllTextAsync();
            try
            {
                string[] splited = textoFromArq.Split('/');
                return splited;
            }
            catch(IndexOutOfRangeException e)
            {

                Debug.Write(textoFromArq);
            }
            return null;

        }

    }
}
