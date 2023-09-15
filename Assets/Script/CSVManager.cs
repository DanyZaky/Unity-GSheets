using UnityEngine;
using System.Collections;
using System.IO;

public class CSVManager : MonoBehaviour
{
    private string filePath;

    private void Start()
    {
        // Menentukan lokasi file CSV di dalam folder Assets
        filePath = Application.dataPath + "/data.csv";

        // Mengecek apakah file CSV sudah ada, jika belum maka membuatnya
        if (!File.Exists(filePath))
        {
            // Menulis header ke file CSV
            string header = "Nama,Nomor HP";
            File.WriteAllText(filePath, header + "\n");
        }
    }

    public void SimpanData(string nama, string nomorHP)
    {
        // Menggabungkan data nama dan nomorHP menjadi baris yang akan ditambahkan ke file CSV
        string data = nama + "," + nomorHP;

        // Menambahkan baris data ke file CSV
        File.AppendAllText(filePath, data + "\n");

        Debug.Log("Data berhasil disimpan: " + data);
    }
}
