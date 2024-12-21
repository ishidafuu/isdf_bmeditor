import os
import zipfile

def zip_files_with_extensions(start_dir, exts, zip_name, include_dirs, exclude_dirs):
    with zipfile.ZipFile(zip_name, 'w', zipfile.ZIP_DEFLATED) as zipf:
        for root, dirs, files in os.walk(start_dir):
            # Exclude directories in exclude_dirs
            dirs[:] = [d for d in dirs if d not in exclude_dirs]
            # Ignore directories not in include_dirs
            dirs[:] = [d for d in dirs if any(os.path.join(root, d).startswith(os.path.join(start_dir, dir)) for dir in include_dirs)]
            for file in files:
                if any(file.endswith(ext) for ext in exts):
                    zipf.write(os.path.join(root, file))

# ディレクトリ、拡張子リスト、zipファイル名を指定します
start_dir = '.'
exts = ['.rs', '.vue', '.css', '.ts', '.html']  # 拡張子をリストで指定します
# 現在のディレクトリ名を取得
current_dir = os.path.basename(os.getcwd())
zip_name = f'{current_dir}.zip'  # 作成するzipファイルの名前を指定します
include_dirs = ['src', 'src-tauri']  # 圧縮対象のディレクトリのリストを指定します
exclude_dirs = ['target']  # 除外するディレクトリのリストを指定します

# zipファイルが存在する場合は削除します
if os.path.exists(zip_name):
    os.remove(zip_name)

zip_files_with_extensions(start_dir, exts, zip_name, include_dirs, exclude_dirs)
