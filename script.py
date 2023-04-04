import subprocess

# Указываем имя директории для виртуального окружения
venv_dir = 'venv'

# Полный путь к исполняемому файлу Python версии 3.9
python_executable = 'D:\\Programs\\Python3.8.0\\python.exe'

# Создаем виртуальное окружение
subprocess.check_call([python_executable, '-m', 'venv', venv_dir])

# Активируем виртуальное окружение
activate_script = venv_dir + '\\Scripts\\activate.bat'
subprocess.check_call(activate_script, shell=True)

# Теперь вы можете установить необходимые пакеты для вашего проекта в этом виртуальном окружении
subprocess.check_call([python_executable, '-m', 'pip', 'install', 'numpy'])
subprocess.check_call([python_executable, '-m', 'pip', 'install', 'tensorflow'])