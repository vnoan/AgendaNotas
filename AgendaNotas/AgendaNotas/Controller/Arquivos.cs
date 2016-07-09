using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;

namespace AgendaNotas.Controller
{
    abstract class Arquivos
    {
        public static async Task<IFile> LoadFile()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("AgendaNotasFiles", CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync("materias.txt", CreationCollisionOption.ReplaceExisting);
            return file;
        }

        public static async Task WriteOnFile(IFile file, string[] materias)
        {
            //Sobrescreve o que está no file
            await file.WriteAllTextAsync(materias.ToString()); 
        }
        
        public static async Task<string[]> ReadOfFile(IFile file)
        {
            string textoFromArq= await file.ReadAllTextAsync();
            string[] splited = textoFromArq.Split('/');
            return splited;
        }

    }
}
