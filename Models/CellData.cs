using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace isdf_bmeditor.Models;

/// <summary>
/// セルデータの保存/読み込みを管理するクラス
/// </summary>
public class CellData
{
    /// <summary>
    /// セルのリスト
    /// </summary>
    public List<Cell> Cells { get; set; } = new();

    /// <summary>
    /// JSONファイルからデータを読み込む
    /// </summary>
    /// <param name="path">JSONファイルのパス</param>
    /// <returns>読み込んだセルデータ</returns>
    public static CellData Load(string path)
    {
        var jsonString = File.ReadAllText(path);
        return JsonSerializer.Deserialize<CellData>(jsonString) ?? new CellData();
    }

    /// <summary>
    /// データをJSONファイルに保存する
    /// </summary>
    /// <param name="path">保存先のパス</param>
    public void Save(string path)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var jsonString = JsonSerializer.Serialize(this, options);
        File.WriteAllText(path, jsonString);
    }
} 