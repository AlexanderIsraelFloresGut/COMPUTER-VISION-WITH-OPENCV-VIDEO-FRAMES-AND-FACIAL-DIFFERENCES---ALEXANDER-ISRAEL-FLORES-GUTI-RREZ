I.	OBJETIVOS DEL PROGRAMA

   Este programa tiene como objetivos:

•	Extraer todos los frames de un video determinado.

•	Calcular todas las diferencias faciales de las personas que aparezcan en un video determinado, a través del Reconocimiento y Análisis de todas las caras de las personas que aparezcan. Para ello, el programa extraerá cada cara como un frame, la convertirá a escala de 
    grises, las convertirá a binarias y luego, calculará la diferencia entre las caras como la diferencia absoluta entre cada una de las caras detectadas y la primera cara detectada.


II.	DATOS IMPORTANTES:

   El nombre de la carpeta raíz es:

    RECONOCIMIENTO_FACIAL_OPENCV - ALEXANDER ISRAEL FLORES GUTIERREZ

  	El tipo de archivo de video que analiza este programa, por el momento es sólo .mp4 

Dentro de la carpeta raíz, se encuentra una carpeta llamada Videos, que es la carpeta que contiene todos los archivos de video .mp4 con los cuales se puede comprobar la funcionalidad del programa. La carpeta contiene unos videos de forma predeterminada. Puede probar el programa con ellos.

El usuario puede perfectamente agregar más videos a esta carpeta.

Este directorio es:

    RECONOCIMIENTO_FACIAL_OPENCV - ALEXANDER ISRAEL FLORES GUTIERREZ\Videos

El programa tiene validada la condición de que el primer video que el usuario seleccione debe tener una duración exacta de 10 segundos. De éste, extraerá y guardará cada frame.

El siguiente video puede ser de cualquier duración. De éste, detectará y analizará todas las caras, ojos, labios, fosas nasales, y otras figuras rectangulares y redondas, calculará la diferencia absoluta entre cada cara y la primera cara detectada y guardará esta diferencia.

	Para guardar todos los frames del video, el programa crea de forma automática una carpeta con el nombre Frames dentro de la carpeta raíz. Este nuevo directorio es

    RECONOCIMIENTO_FACIAL_OPENCV - ALEXANDER ISRAEL FLORES GUTIERREZ\Frames

	Para guardar todas las diferencias faciales, el programa crea de forma automática una carpeta con el nombre Diferencias_Faciales dentro de la carpeta raíz del programa. Este nuevo directorio es

    RECONOCIMIENTO_FACIAL_OPENCV - ALEXANDER ISRAEL FLORES GUTIERREZ\ Diferencias_Faciales  


III. FUNCIONALIDAD DEL PROGRAMA:

Al ejecutar el programa, éste detectará de forma automática todos los archivos de video .mp4 que se encuentren en la carpeta Videos, le solicitará al usuario que seleccione uno de estos videos, mostrará cada frame del video en una nueva ventana y guardará todos los frames de este video en la carpeta Frames.

Luego le solicitará al usuario que seleccione otro video, y de cada frame de este segundo video, detectará y analizará las caras, ojos, labios, fosas nasales, etc. de las personas que aparezcan, y otras figuras rectangulares y redondas, mostrando todos estos elementos en una nueva ventana. Al mismo tiempo, calculará la diferencia absoluta entre las caras detectadas, como la diferencia entre cada cara que se detecte y la primera cara detectada y guardará cada una de estas diferencias en la carpeta Diferencias_Faciales.

Pulse en cualquier momento la tecla q para detener la detección y el guardado de frames, o para detener la detección de figuras rectangulares o redondas y el guardado de las diferencias faciales.




Con mucho placer en poder servir,




Ing. Alexander Israel Flores Gutiérrez

Managua, Nicaragua
