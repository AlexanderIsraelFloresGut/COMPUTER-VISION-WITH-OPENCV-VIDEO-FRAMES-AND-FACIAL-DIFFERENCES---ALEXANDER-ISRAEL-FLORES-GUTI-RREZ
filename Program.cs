//**************************************DESARROLLADO POR: ING.ALEXANDER ISRAEL FLORES GUTIÉRREZ**************************************//
//*********************************************************MANAGUA, NICARAGUA*********************************************************//

using OpenCvSharp;

Console.WriteLine($@"--------------***************DESARROLLADO POR: ING. ALEXANDER ISRAEL FLORES GUTIÉRREZ-----------------------**************************
----------------------------*******************MANAGUA, NICARAGUA-------------------------------------********************************");

Console.WriteLine($@"
-----------*********¡HOLA, BIENVENIDO AL GENERADOR DE FRAMES Y DIFERENCIAS FACIALES A PARTIR DE ARCHIVOS DE VIDEO!-----------*********
---------------------***************************¡CON RECONOCIMIENTO FACIAL EN TIEMPO REAL!---------------------***********************

-------------> ATENCIÓN!: Presiona la tecla q cuando desees detener la generación de frames o de diferencias faciales. <--------------");


//INICIALIZACIÓN DE CADA UNA DE LAS VARIABLES A UTILIZAR

var video_seleccionado = new OpenCvSharp.VideoCapture();
IEnumerable<string> Lista_De_Videos = System.IO.Directory.EnumerateFiles($@"..\..\..\Videos", "*.mp4");
int opcion;
double duracion_del_video_en_segundos = 0;
Mat frame = new();
int NumeroDeFrame = 0;
var Detector_De_Figuras_Rectangulares = new OpenCvSharp.CascadeClassifier(@"..\..\..\OpenCV_Archivos\haarcascade_frontalface_default.xml");
var Detector_De_Figuras_Redondas = new OpenCvSharp.CascadeClassifier(@"..\..\..\OpenCV_Archivos\haarcascade_eye.xml");
var Imagen_Gris = new Mat();
var primera_cara = new Mat();
var cara_actual = new Mat();
var Diferencia_Facial = new Mat();
var rectangulo_color = Scalar.FromRgb(255, 0, 0);
var figura_circular_color = Scalar.FromRgb(0, 0, 255);
var Imagen_Gris_De_Cara_Detectada = new Mat();
var cara_imagen_gris_binaria = new Mat();


/*PRIMERO VAMOS A EXTRAER TODOS LOS FRAMES DE UN VIDEO QUE TENGA 10 SEGUNDOS DE DURACIÓN. ESTA ES UNA CONDICIÓN QUE ESTÁ
  VALIDADA EN ESTE PROGRAMA SÓLO PARA EL PRIMER VIDEO. SE LE SEÑALA AL USUARIO LOS VIDEOS QUE TIENEN ESTA DURACIÓN. */

Console.WriteLine($@"

Vamos a extraer todos los frames de un video. Selecciona uno de los siguientes.
(Por ahora debes seleccionar uno de 10 segundos):

");
int i = 0;
foreach (var video in Lista_De_Videos)
{
    i++;
    video_seleccionado = new OpenCvSharp.VideoCapture(video);
    duracion_del_video_en_segundos = Math.Round((video_seleccionado.FrameCount / video_seleccionado.Fps), MidpointRounding.ToEven);
    if (duracion_del_video_en_segundos == 10)
    {
        Console.WriteLine($@"{i}. {video.Substring(16)}              <-------- Este video es de 10 segundos");
    }
    else
    {
        Console.WriteLine($@"{i}. {video.Substring(16)}");
    }
}

Console.WriteLine($@"

Introduce el número de un video:

");
opcion = int.Parse(Console.ReadLine());
video_seleccionado = new OpenCvSharp.VideoCapture(Lista_De_Videos.ElementAt(opcion - 1));
duracion_del_video_en_segundos = Math.Round((video_seleccionado.FrameCount / video_seleccionado.Fps), MidpointRounding.ToEven);
Console.WriteLine($@"

El video seleccionado es: {opcion}. {Lista_De_Videos.ElementAt(opcion - 1).Substring(16)} de {duracion_del_video_en_segundos} segundos.");

while (duracion_del_video_en_segundos != 10)
{
    Console.WriteLine($@"
Por favor selecciona un video de 10 segundos.

Introduce el número de un video:
                       ");

    i = 0;
    foreach (var video in Lista_De_Videos)
    {
        i++;
        video_seleccionado = new OpenCvSharp.VideoCapture(video);
        duracion_del_video_en_segundos = Math.Round((video_seleccionado.FrameCount / video_seleccionado.Fps), MidpointRounding.ToEven);
        if (duracion_del_video_en_segundos == 10)
        {
            Console.WriteLine($@"{i}. {video.Substring(16)}              <-------- Este video es de 10 segundos");
        }
        else
        {
            Console.WriteLine($@"{i}. {video.Substring(16)}");
        }
    }

    Console.WriteLine($@"

");

    opcion = int.Parse(Console.ReadLine());
    video_seleccionado = new OpenCvSharp.VideoCapture(Lista_De_Videos.ElementAt(opcion - 1));
    duracion_del_video_en_segundos = Math.Round((video_seleccionado.FrameCount / video_seleccionado.Fps), MidpointRounding.ToEven);
    Console.WriteLine($@"

El video seleccionado es: {opcion}. {Lista_De_Videos.ElementAt(opcion - 1).Substring(16)} de {duracion_del_video_en_segundos} segundos.");
}

var Carpeta_Frames = System.IO.Directory.CreateDirectory($@"..\..\..\Frames");

var Interfaz_Video = new Window($@"Alexander Israel Flores Gutiérrez - OpenCVSharp - Guardar todos los Frames del Video {Lista_De_Videos.ElementAt(opcion - 1).Substring(16)}");

NumeroDeFrame = 0;

while (video_seleccionado.IsOpened())
{
    video_seleccionado.Read(frame);

    if (frame.Empty())
        break;

    NumeroDeFrame++;

    var NombreFrame = $@"{Carpeta_Frames.FullName}\Frame_No._{NumeroDeFrame}.jpg";
    Cv2.ImWrite(NombreFrame, frame);

    Interfaz_Video.ShowImage(frame);
    if (Cv2.WaitKey(1) == 113) // PRESIONAR q PARA DETENER.
        break;
}

Cv2.DestroyAllWindows();

Console.WriteLine($@"

El video seleccionado fue: {opcion}. {Lista_De_Videos.ElementAt(opcion - 1).Substring(16)} de {duracion_del_video_en_segundos} segundos.");


Console.WriteLine($@"

----------->¡ÉXITO!<-----------

Todos los frames del video fueron extraídos y guardados correctamente en esta ruta:

{Carpeta_Frames.FullName}");

/*AHORA VAMOS A EXTRAER TODAS LAS DFERENCIAS FACIALES DE UNA PERSONA QUE APAREZCA EN UN VIDEO.
 LA IDEA ES DIFERENCIAR LA CARA DE LA PERSONA EN EL PRIMER FRAME, DE LA CARA DE LA MISMA PERSONA EN CADA UNO DE LOS DEMÁS FRAMES.
 */

Console.WriteLine($@"

Ahora vamos a extraer todas las diferencias faciales de una persona, utilizando reconocimiento facial en tiempo real.
Las diferencias son entre la cara del primer fotograma, y la cara de cada uno de los demás fotogramas en el video.
Estas diferencias serán obtenidas a partir de las caras convertidas a escala de grises y binarizadas.
Selecciona un video (Esta vez puedes seleccionar cualquiera, RECUERDA que pulsando la tecla q puedes detener el proceso):

");

int j = 0;
foreach (var video in Lista_De_Videos)
{
    j++;
    Console.WriteLine($@"{j}. {video.Substring(16)}");
}

Console.WriteLine($@"

Introduce el número de un video:

");

opcion = int.Parse(Console.ReadLine());
video_seleccionado = new OpenCvSharp.VideoCapture(Lista_De_Videos.ElementAt(opcion - 1));
duracion_del_video_en_segundos = Math.Round((video_seleccionado.FrameCount / video_seleccionado.Fps), MidpointRounding.ToEven);

var Carpeta_Diferencias_Faciales = System.IO.Directory.CreateDirectory($@"..\..\..\Diferencias_Faciales");

var Interfaz_Video_Persona = new Window($@"Alexander Israel Flores Gutiérrez - OpenCVSharp - Guardar todas las Diferencias Faciales del Video {Lista_De_Videos.ElementAt(opcion - 1).Substring(16)}");
NumeroDeFrame = 0;
while (video_seleccionado.IsOpened())
{
    video_seleccionado.Read(frame);

    if (frame.Empty())
        break;

    Cv2.CvtColor(frame, Imagen_Gris, ColorConversionCodes.BGRA2GRAY);
    Cv2.EqualizeHist(Imagen_Gris, Imagen_Gris);

    var rectangulos_caras = Detector_De_Figuras_Rectangulares.DetectMultiScale(
        image: Imagen_Gris,
        minSize: new Size(60, 60));

    foreach (var rectangulo_cara in rectangulos_caras)
    {
        cara_actual = new(frame, rectangulo_cara);
        Cv2.Rectangle(frame, rectangulo_cara, rectangulo_color, 3);
        Cv2.CvtColor(cara_actual, Imagen_Gris_De_Cara_Detectada, ColorConversionCodes.BGRA2GRAY);
        var Figuras_Redondas = Detector_De_Figuras_Redondas.DetectMultiScale(
           image: Imagen_Gris_De_Cara_Detectada,
           minSize: new OpenCvSharp.Size(30, 30));

        Cv2.Threshold(Imagen_Gris_De_Cara_Detectada, cara_imagen_gris_binaria, 0, 255, ThresholdTypes.Triangle | ThresholdTypes.Binary);

        if (NumeroDeFrame == 0)
        {
            primera_cara = cara_imagen_gris_binaria.Clone();
        }

        cara_actual = cara_imagen_gris_binaria.Clone();


        foreach (var Figura_Redonda in Figuras_Redondas)
        {
            var centro = new OpenCvSharp.Point
            {
                X = (int)(Math.Round(Figura_Redonda.X + Figura_Redonda.Width * 0.5, MidpointRounding.ToEven) + rectangulo_cara.Left),
                Y = (int)(Math.Round(Figura_Redonda.Y + Figura_Redonda.Height * 0.5, MidpointRounding.ToEven) + rectangulo_cara.Top)
            };
            var radio = Math.Round((Figura_Redonda.Width + Figura_Redonda.Height) * 0.25, MidpointRounding.ToEven);
            Cv2.Circle(frame, centro, (int)radio, figura_circular_color, thickness: 2);
        }
    }

    Interfaz_Video_Persona.ShowImage(frame);

    cara_actual.Reshape(primera_cara.Channels());
    Cv2.Resize(cara_actual, cara_actual, primera_cara.Size());
    Cv2.Absdiff(cara_actual, primera_cara, Diferencia_Facial);
    NumeroDeFrame++;
    var Nombre_De_Diferencia_Facial = $@"{Carpeta_Diferencias_Faciales.FullName}\Diferencia_Facial_No._{NumeroDeFrame}.jpg";
    Cv2.ImWrite(Nombre_De_Diferencia_Facial, Diferencia_Facial);
    if (Cv2.WaitKey(1) == 113) // PRESIONAR q PARA DETENER.
        break;
}

Cv2.DestroyAllWindows();

Console.WriteLine($@"

El video seleccionado fue: {opcion}. {Lista_De_Videos.ElementAt(opcion - 1).Substring(16)} de {duracion_del_video_en_segundos} segundos.");

Console.WriteLine($@"

----------->¡ÉXITO!<-----------

Todos las diferencias faciales fueron guardadas correctamente en esta ruta:

{Carpeta_Diferencias_Faciales.FullName}");


Console.WriteLine($@"

¡TODO LISTO, POR ESTA OCASIÓN, ESTO ES TODO, HASTA MUY PRONTO!");