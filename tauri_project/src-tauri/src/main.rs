// Prevents additional console window on Windows in release, DO NOT REMOVE!!
#![cfg_attr(not(debug_assertions), windows_subsystem = "windows")]

use tauri::Manager;

fn main() {
  tauri::Builder::default()
      .setup(|app| {
        let _window = app.get_window("main").unwrap();
        Ok(())
      })
      .invoke_handler(tauri::generate_handler![open_save_directory])
      .run(tauri::generate_context!())
      .expect("error while running tauri application");
}

#[tauri::command]
fn open_save_directory(path: String) -> Result<(), String> {
    std::process::Command::new("explorer")
        .arg(path)
        .spawn()
        .map_err(|e| e.to_string())?;
    Ok(())
}