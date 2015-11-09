using System;
using System.Drawing;
using System.Threading.Tasks;



namespace compundEye
{
    public enum HexOrientation
    {
        /// <summary>
        /// Hex positioned with flat top and bottom
        /// </summary>
        Flat = 0,
        /// <summary>
        /// Hex positioned with points at top and botom
        /// </summary>
        Pointy = 1,
    }

    public enum PointyVertice
    {
        Top = 0,
        UpperRight = 1,
        BottomRight = 2,
        Bottom = 3,
        BottomLeft = 4,
        TopLeft = 5,
    }

    public enum FlatVertice
    {
        UpperLeft = 0,
        UpperRight = 1,
        MiddleRight = 2,
        BottomRight = 3,
        BottomLeft = 4,
        MiddleLeft = 5,
    }

    public class Hex
    {
        private PointF[] points; // Vertices
        // Parametros de Hexagono
        private float side; //  side = length of a side of the hexagon, all 6 are equal length
        private float h; //  h = short length (outside)
        private float r; //  r = long length (outside)
        private HexOrientation orientation;
        private float x; //center Pix
        private float y; //center Pix
        public Color[] colors = new Color[3];
        private Rectangle afectRect;
        public int nfila, ncolumna, nHexagono;

        public Hex(int x, int y, float side, float apotema, HexOrientation orientation)
        {
            Initialize((float)(x), (float)(y), side, apotema, orientation);
        }

        public Hex(float x, float y, float side, float apotema, HexOrientation orientation)
        {
            Initialize(x, y, side, apotema, orientation);
        }

        public Hex(PointF point, float side, float apotema, HexOrientation orientation)
        {
            Initialize(point.X, point.Y, side, apotema, orientation);
        }


        /// <summary>
        /// Sets internal fields and calls CalculateVertices()
        /// </summary>
        private void Initialize(float x, float y, float side, float? apotema, HexOrientation orientation)
        {
            this.x = x;
            this.y = y;
            this.side = side;

            this.orientation = orientation;
            this.colors[0] = Color.White;
            if (apotema.HasValue) { CalculateVertices(side/2, apotema.Value); }
            else CalculateVertices();
        }

        /// <summary>
        /// Calculates the vertices of the hex based on orientation. Assumes that points[0] contains a value.
        /// </summary>
        private void CalculateVertices(float h, float r)
        {
            //  
            //  h = short length (outside)
            //  r = long length (outside)
            //  side = length of a side of the hexagon, all 6 are equal length
            //
            //  h = sin (30 degrees) x side
            //  r = cos (30 degrees) x side
            //
            //		 h
            //	     ---
            //   ----     |r
            //  /    \    |          
            // /      \   |
            // \      /
            //  \____/
            //
            // Flat orientation (scale is off)
            //
            //     /\
            //    /  \
            //   /    \
            //   |    |
            //   |    |
            //   |    |
            //   \    /
            //    \  /
            //     \/
            // Pointy orientation (scale is off)
            
            switch (orientation)
            {
                case HexOrientation.Flat:
                    // x,y coordinates are top left point
                    //points = new System.Drawing.PointF[6];
                    //points[0] = new PointF(x, y);
                    //points[1] = new PointF(x + Side, y);
                    //points[2] = new PointF(x + Side + h, y + r);
                    //points[3] = new PointF(x + Side, y + r + r);
                    //points[4] = new PointF(x, y + r + r);
                    //points[5] = new PointF(x - h, y + r);
                    // x,y coordinates are center point                    
                    points = new System.Drawing.PointF[6];
                    points[0] = new PointF(x - h, y - r);
                    points[1] = new PointF(x + h, y - r);
                    points[2] = new PointF(x + Side, y);
                    points[3] = new PointF(x + h, y + r);
                    points[4] = new PointF(x - h, y + r);
                    points[5] = new PointF(x - Side, y);
                    break;
                case HexOrientation.Pointy:
                    //x,y coordinates are top center point
                    //points = new System.Drawing.PointF[6];
                    //points[0] = new PointF(x, y);
                    //points[1] = new PointF(x + r, y + h);
                    //points[2] = new PointF(x + r, y + Side + h);
                    //points[3] = new PointF(x, y + Side + h + h);
                    //points[4] = new PointF(x - r, y + Side + h);
                    //points[5] = new PointF(x - r, y + h);
                    // x,y coordinates are center point
                    points = new System.Drawing.PointF[6];
                    points[0] = new PointF(x - r, y - h);
                    points[1] = new PointF(x, y - Side);
                    points[2] = new PointF(x + r, y - h);
                    points[3] = new PointF(x + r, y + h);
                    points[4] = new PointF(x, y + Side);
                    points[5] = new PointF(x - r, y + h);
                    break;
                default:
                    throw new Exception("No HexOrientation defined for Hex object.");
            }
        }

        private void CalculateVertices()
        {
            h = Side / 2f; // Side/2
            r = (float)(Side * Math.Sqrt(3) / 2); // apotema del hex 
            CalculateVertices(h, r);
        }

        #region Properties

        public HexOrientation Orientation
        {
            get
            {
                return orientation;
            }
            set
            {
                orientation = value;
            }
        }
        public System.Drawing.PointF[] Points
        {
            get
            {
                return points;
            }
            set
            {
            }
        }
        public float Side
        {
            get
            {
                return side;
            }
            set
            {
            }
        }
        public float H
        {
            get
            {
                return h;
            }
            set
            {
            }
        }
        public float R
        {
            get
            {
                return r;
            }
            set
            {
            }
        }
        public float X
        {
            get
            {
                return x;
            }
            set
            {
            }
        }
        public float Y
        {
            get
            {
                return y;
            }
            set
            {
            }
        }
        public Rectangle AfectRect
        {
            get
            {
                return afectRect;
            }
            set
            {
                afectRect = value;
            }
        }
        #endregion Properties
    }

    public class ojoCompuesto
    {
        private static ojoCompuesto Ojo;

        // parametros fisicos Homatidios
        private double _angVertical;
        private double _angAceptacion;
        private double alfamax;
        // parametros imagen Real        
        private string _imgFile;
        public Bitmap inImage;
        private double _distanciaCm; // Distancia a la imagen visualizada
        private double _alturaCm; // Altura de la imagen visualizada
        private double anchuraCm; // Anchura de la imagen visualizada
        private int Ni_alturaPix, Nj_anchuraPix; //nº pixels de la imagen
        private float escala;// factor conversion cm en pixel

        // parametros imagen Compuesta
        public Bitmap outImage; // Imagen procesada

        // propiedades de los Hexagonos
        private float hexLadoCm; // Lado del Hex
        private double hexApotemaCm, hexAnchuraCm, hexAlturaCm, hexCuadradoCm;
        private int hexLadoPix,hexApotemaPix ,hexAnchuraPix, hexAlturaPix, hexCuadradoPix;
        public int nfilas, ncolumnas, nHexagonos;

        // Hexagonos
      //  private Hex[][] HexesCm;
        public  Hex[][] HexesPix;

        #region Properties
        public double angVertical
        {
            get
            {
                return _angVertical;
            }
            set
            {
                _angVertical = value * Math.PI / 180; ;
            }
        }

        public double angAceptacion
        {
            get
            {
                return _angAceptacion;
            }
            set
            {
                _angAceptacion = value * Math.PI / 180; ;
            }
        }
        public double distanciaCm
        {
            get
            {
                return _distanciaCm;
            }
            set
            {
                _distanciaCm = value;
            }
        }
        public double alturaCm
        {
            get
            {
                return _alturaCm;
            }
            set
            {
                _alturaCm = value;
            }
        }
        public string imgFile
        {
            get
            {
                return _imgFile;
            }
            set
            {
                _imgFile = value;
                loadImage();
            }
        }
        private void loadImage()
        {
            try
            {
                inImage = new Bitmap(imgFile);
            }
            catch
            {
                //exce`cion 
            }
        }

        #endregion

        // singleton
        public static ojoCompuesto Instance()
        {
            if (Ojo == null)
            {
                Ojo = new ojoCompuesto();
            }

            return Ojo;
        }

        public void InicialiceOjo(Image inImage, float AngVertical, float AngAceptacion, float Distancia, float AlturaCm)
        {
            //
            Bitmap newBmp = new Bitmap(inImage);

            // convertir formato de imagen a 24bppRgb
            if (inImage.PixelFormat.ToString() != "Format24bppRgb")
            {             
                Bitmap oldBmp = new Bitmap(inImage);
                Bitmap auxBmp = new Bitmap(oldBmp);
                newBmp = auxBmp.Clone(new Rectangle(0, 0, auxBmp.Width, auxBmp.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                
            }

            this.inImage = new Bitmap(newBmp);
            this.outImage = new Bitmap(newBmp);

            angVertical = AngVertical;
            angAceptacion = AngAceptacion;
            distanciaCm = Distancia;
            alturaCm = AlturaCm;

            InicialiceOjo();
        }
        
        public void InicialiceOjo()
        {
            // nº pixeles imagen (px)
            Ni_alturaPix = inImage.Height;
            Nj_anchuraPix = inImage.Width;

            // Anchura imagen (cm)                        
            anchuraCm = alturaCm * Nj_anchuraPix / Ni_alturaPix;
            escala = (float)(Nj_anchuraPix / anchuraCm); // factor conversion cm en pixel

            // parametros calculo tamaño Hexagono
            // 5 a 30cm para distancia , y 0.5º a 1º para angulos

            double phi_v = angVertical;
            double rho = angAceptacion;

            // tamaño Hexagono 
            // double pp = 4 / 3f * distanciaCm * Math.Tan(phi_v / 2);
            double pp = 2 / Math.Sqrt(3) * distanciaCm * Math.Tan(phi_v / 2);
            hexLadoCm = (float)pp;
            // hexApotemaCm = L_hexLadoCm /2f * Math.Sin(60*Math.PI / 180);
            hexApotemaCm = hexLadoCm * Math.Sqrt(3) / 2;
            hexApotemaPix = Convert.ToInt32(hexApotemaCm * escala);
            hexAnchuraCm = 2 * hexApotemaCm;
            hexAlturaCm = 2 * hexLadoCm;
            hexLadoPix = Convert.ToInt32(hexLadoCm * escala); // redondeado a pixels par
            if (hexLadoPix % 2 != 0) { hexLadoPix = hexLadoPix + 1; }
            hexAlturaPix = 2 * hexLadoPix; // en pixels 
            hexAnchuraPix = 2 * Convert.ToInt32(hexApotemaCm * escala); // en pixels 

            // nº hexagonos en imagen            
            nfilas = Convert.ToInt32(Math.Ceiling(Ni_alturaPix / (1.5 * hexLadoPix))) + 1;
            ncolumnas = Convert.ToInt32(Math.Ceiling((float)Nj_anchuraPix / hexAnchuraPix)) + 1;
            nHexagonos = nfilas * ncolumnas;

            // cuadrado de covertura del hexagono
            hexCuadradoCm = 2 * distanciaCm * Math.Tan(2 * rho) / 2;
            hexCuadradoPix = Convert.ToInt32(Math.Ceiling(hexCuadradoCm * escala));
            double H2 = alturaCm / 2;
            double B2 = anchuraCm / 2;
            alfamax = 1.5 * rho;

            // centros 
            // calculate centers of the hexagons
            HexesPix = new Hex[ncolumnas][];
            for (int iaux = 0; iaux < ncolumnas; iaux++) { HexesPix[iaux] = new Hex[nfilas]; }            
            int xPix, yPix;
            for (int i = 0; i < ncolumnas; i++)
            {
                for (int j = 0; j < nfilas; j++)
                {
                    if (j % 2 != 0) // si fila es impar
                    {
                        xPix = hexAnchuraPix * i + hexApotemaPix;
                    }
                    else
                    {
                        xPix= hexAnchuraPix * i;
                    }

                    yPix = Convert.ToInt32(3f/2f * hexLadoPix * j);

                    HexesPix[i][j] = new Hex(xPix, yPix, hexLadoPix, hexApotemaPix, HexOrientation.Pointy);
                    HexesPix[i][j].AfectRect = new Rectangle((int)(xPix - hexCuadradoPix / 2f), (int)(yPix - hexCuadradoPix / 2f), hexCuadradoPix, hexCuadradoPix);
                    HexesPix[i][j].nfila = j;
                    HexesPix[i][j].ncolumna = i;
                    HexesPix[i][j].nHexagono = i * nfilas + j;
                }
            }



            // centros 
            // calculate centers of the hexagons
            //HexesCm = new Hex[ncolumnas][];
            //for (int iaux = 0; iaux < ncolumnas; iaux++) { HexesCm[iaux] = new Hex[nfilas]; }
            //HexesPix = new Hex[ncolumnas][];
            //for (int iaux = 0; iaux < ncolumnas; iaux++) { HexesPix[iaux] = new Hex[nfilas]; }
            //float x, y;
            //int xPix, yPix;
            //for (int i = 0; i < ncolumnas; i++)
            //{
            //    for (int j = 0; j < nfilas; j++)
            //    {
            //        if (j % 2 != 0)
            //        {
            //            x = (float)hexAnchuraCm * (i + 0.5f);
            //        }
            //        else
            //        {
            //            x = (float)hexAnchuraCm * i;

            //        }
            //        y = 3f / 2f * L_hexLadoCm * j;

            //        xPix = Convert.ToInt32(Math.Ceiling(x * escala));
            //        yPix = Convert.ToInt32(Math.Ceiling(y * escala));

            //        HexesCm[i][j] = new Hex(x, y, L_hexLadoCm, HexOrientation.Pointy);
            //        HexesPix[i][j] = new Hex(xPix, yPix, L_hexLadoPix, HexOrientation.Pointy);
            //        HexesPix[i][j].AfectRect = new Rectangle((int)(xPix - hexCuadradoPix / 2f), (int)(yPix - hexCuadradoPix / 2f), hexCuadradoPix, hexCuadradoPix);
            //        HexesPix[i][j].nfila = j;
            //        HexesPix[i][j].ncolumna = i;
            //        HexesPix[i][j].nHexagono = i * nfilas + j;
            //    }
            //}


        }

        public void calccolors()
        {
            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, inImage.Width, inImage.Height);
            System.Drawing.Imaging.BitmapData bmpData = inImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, inImage.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * inImage.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            // set variable local para inimage.with
            int iWidth = inImage.Width;
            int iHeight = inImage.Height;

           

            int bytexPixel = Image.GetPixelFormatSize(inImage.PixelFormat)/8;

            //For (int i = 0; i < ncolumnas; i++) prueba en paralelo
          //  Parallel.For(0, ncolumnas, i =>
            for (int i = 0; i < ncolumnas; i++)
            {
                for (int j = 0; j < nfilas; j++)
                {
                    double numR = 0, numG = 0, numB = 0;

                    int xmin = HexesPix[i][j].AfectRect.X;
                    int ymin = HexesPix[i][j].AfectRect.Y;
                    int lado = HexesPix[i][j].AfectRect.Width;

                    int H = Nj_anchuraPix / 2; // centro 
                    int B = Ni_alturaPix / 2; // del
                    double D = (distanciaCm * escala); //ojo

                    int Xx = (int)HexesPix[i][j].X;
                    int Yx = (int)HexesPix[i][j].Y;

                    double peso = 0, denom = 0;

                    Color aux = new Color();

                    for (int ix = 0; ix < lado; ix++)
                    {
                        for (int iy = 0; iy < lado; iy++)
                        {
                            int px = xmin + ix;
                            int py = ymin + iy;
                            if (px < 0 | px >= iWidth) continue;
                            if (py < 0 | py >= iHeight) continue;

                            double auxX = Xx - H;
                            double auxY = Yx - B;
                            double auxx = px - H;
                            double auxy = py - B;
                            double prod_esc = auxX * auxx + auxY * auxy + D * D;
                            double mod_u = Math.Sqrt(auxX * auxX + auxY * auxY + D * D);
                            double mod_v = Math.Sqrt(auxx * auxx + auxy * auxy + D * D);
                            double cosang = prod_esc / (mod_u * mod_v);
                            if (cosang > 1) cosang = 1;
                            if (cosang < 0) cosang = 0;
                            double ang = Math.Acos(cosang);
                            if (ang >= 1.5 * angAceptacion) continue;
                            peso += Math.Exp(-2.77 * Math.Pow(ang / angAceptacion, 2));

                            int npixel = py * iWidth * bytexPixel + px * bytexPixel;
                            numR += rgbValues[npixel + 0] * peso;
                            numG += rgbValues[npixel + 1] * peso;
                            numB += rgbValues[npixel + 2] * peso;

                            denom += peso;

                        }
                    }
                    if (peso > 0)
                    {
                        int cR = (int)(numR / denom);
                        int cG = (int)(numG / denom);
                        int cB = (int)(numB / denom);
                        aux = Color.FromArgb(255, cB, cG, cR);
                    }
                    else { aux = Color.FromArgb(255, 0, 0, 0); }
                    HexesPix[i][j].colors[2] = aux;
                    // TODO : LANZAR UN mensagge de hexzagono realizado... para log de tiempo
                }
            };

            // Unlock the bits.
            inImage.UnlockBits(bmpData);
        }

        public Image Paint(Boolean hexagonos, Boolean Rectangulos, Boolean beeview, int type)
        {
            Image aux = new Bitmap(inImage);
            Graphics g = Graphics.FromImage(aux);

            Pen p = new Pen(Color.Black);
            SolidBrush sb = new SolidBrush(Color.Black);

            p.Color = Color.Black;
            p.Width = 1;
            // indice rectangulo central
            int rcx = (ncolumnas-1) / 2;
            int rcy = (nfilas-1) / 2;


            for (int i = 0; i < ncolumnas; i++)
            {
                for (int j = 0; j < nfilas; j++)
                {
                    if (beeview)
                    {
                        p.Color = HexesPix[i][j].colors[type];
                        g.FillPolygon(new SolidBrush(p.Color), HexesPix[i][j].Points);
                        g.DrawPolygon(p, HexesPix[i][j].Points);
                    }
                    //if (Rectangulos & i==rcx & j==rcy)
                    //{
                    //    // pintar rectangulo de vision de muestra 
                    //    // pintar solo el rectangulo central
                        
                    //    //p.Color = Color.Red;
                    //    //p.Width = 2;
                    //    //Rectangle rect = new Rectangle((int)(HexesPix[i][j].X - hexCuadradoPix / 2), (int)(HexesPix[i][j].Y - hexCuadradoPix / 2), hexCuadradoPix, hexCuadradoPix);
                    //    //g.DrawRectangle(p, rect);
                    //}
                    if (hexagonos)
                    {
                        p.Color = Color.White;
                        g.DrawPolygon(p, HexesPix[i][j].Points);
                    }


                }
            }
            // pintar rectangulo de vision de muestra 
            // pintar solo el rectangulo mas central
            if (Rectangulos)
            {
                p.Color = Color.Red;
                p.Width = 2;
                Rectangle rect = new Rectangle((int)(HexesPix[rcx][rcy].X - hexCuadradoPix / 2), (int)(HexesPix[rcx][rcy].Y - hexCuadradoPix / 2), hexCuadradoPix, hexCuadradoPix);
                g.DrawRectangle(p, rect);
                g.DrawEllipse(p, HexesPix[rcx][rcy].X, HexesPix[rcx][rcy].Y, 5, 5);
            }
            return aux;
        }

/*        private Color HexColor(int i, int j, int type)
        {
            Color aux = new Color();
            switch (type)
            {
                case 0:
                    {
                        //  color del punto medio      
                        int numR = 0, numG = 0, numB = 0;
                        Color pntMed_c = new Color();
                        int x = (int)HexesPix[i][j].X;
                        int y = (int)HexesPix[i][j].Y;
                        if (x > 0 & x < inImage.Width & y > 0 & y < inImage.Height)
                        {
                            numR = inImage.GetPixel(x, y).R;
                            numG = inImage.GetPixel(x, y).G;
                            numB = inImage.GetPixel(x, y).B;
                            pntMed_c = Color.FromArgb(255, numR, numG, numB);
                        }
                        else
                        {
                            pntMed_c = Color.FromArgb(255, 0, 0, 0);
                        }
                        aux = pntMed_c;
                        break;
                    }
                case 1:
                    {
                        // Color Medio del
                        // cuadrado de inspeccion
                        int xmin = HexesPix[i][j].AfectRect.X;
                        int ymin = HexesPix[i][j].AfectRect.Y;
                        int lado = HexesPix[i][j].AfectRect.Width;

                        Color ixiy_C = new Color();
                        float R = 0, G = 0, B = 0;

                        for (int ix = 0; ix < lado; ix++)
                        {
                            for (int iy = 0; iy < lado; iy++)
                            {
                                int px = xmin + ix;
                                int py = ymin + iy;
                                if (px < 0 | px >= inImage.Width) continue;
                                if (py < 0 | py >= inImage.Height) continue;

                                ixiy_C = inImage.GetPixel(px, py);
                                R = (R + ixiy_C.R);
                                G = (G + ixiy_C.G);
                                B = (B + ixiy_C.B);
                            }
                        }
                        R = R / (lado * lado);
                        G = G / (lado * lado);
                        B = B / (lado * lado);
                        aux = Color.FromArgb(255, (int)R, (int)G, (int)B);
                        break;
                    }
                case 2:
                    {
                        double numR = 0, numG = 0, numB = 0, numA = 0;

                        int xmin = HexesPix[i][j].AfectRect.X;
                        int ymin = HexesPix[i][j].AfectRect.Y;
                        int lado = HexesPix[i][j].AfectRect.Width;

                        int H = Nj_anchuraPix / 2; // centro 
                        int B = Ni_alturaPix / 2; // del
                        int D = (int)(distanciaCm * escala); //ojo

                        int Xx = (int)HexesPix[i][j].X;
                        int Yx = (int)HexesPix[i][j].Y;

                        double peso = 0, denom = 0;

                        // Lock the bitmap's bits.  

                        Rectangle rect = new Rectangle(xmin, ymin, lado, lado);
                        System.Drawing.Imaging.BitmapData bmpData = inImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, inImage.PixelFormat);

                        // Get the address of the first line.
                        IntPtr ptr = bmpData.Scan0;

                        // Declare an array to hold the bytes of the bitmap.
                        int bytes = Math.Abs(bmpData.Stride) * inImage.Height;
                        byte[] rgbValues = new byte[bytes];

                        // Copy the RGB values into the array.
                        System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

                        int npixel = 0;

                        for (int ix = 0; ix < lado; ix++)
                        {
                            for (int iy = 0; iy < lado; iy++)
                            {
                                int px = xmin + ix;
                                int py = ymin + iy;
                                if (px < 0 | px >= inImage.Width) continue;
                                if (py < 0 | py >= inImage.Height) continue;

                                double auxX = Xx - H;
                                double auxY = Yx - B;
                                double auxx = px - H;
                                double auxy = py - B;
                                double prod_esc = auxX * auxx + auxY * auxy + D * D;
                                double mod_u = Math.Sqrt(auxX * auxX + auxY * auxY + D * D);
                                double mod_v = Math.Sqrt(auxx * auxx + auxy * auxy + D * D);
                                double cosang = prod_esc / (mod_u * mod_v);
                                if (cosang > 1) cosang = 1;
                                if (cosang < 0) cosang = 0;
                                double ang = Math.Acos(cosang);
                                if (ang >= 1.5 * angAceptacion) continue;
                                peso += Math.Exp(-2.77 * Math.Pow(ang / angAceptacion, 2));

                                numR += rgbValues[npixel] * peso;
                                numG += rgbValues[npixel + 1] * peso;
                                numB += rgbValues[npixel + 2] * peso;
                                numA += rgbValues[npixel + 3] * peso;

                                denom += peso;
                                npixel += 4;
                            }
                        }
                        if (peso > 0)
                        {
                            int cR = (int)(numR / denom);
                            int cG = (int)(numG / denom);
                            int cB = (int)(numB / denom);
                            aux = Color.FromArgb(255, cR, cG, cB);
                        }
                        else { aux = Color.FromArgb(255, 0, 0, 0); }

                        // Unlock the bits.
                        inImage.UnlockBits(bmpData);

                        break;
                    }
            }

            return aux;
        }
        */

        public static bool InsidePolygon(PointF[] polygon, int N, PointF p)
        {
            //http://astronomy.swin.edu.au/~pbourke/geometry/insidepoly/
            //
            // Slick algorithm that checks if a point is inside a polygon.  Checks how may time a line
            // origination from point will cross each side.  An odd result means inside polygon.
            //
            int counter = 0;
            int i;
            double xinters;
            PointF p1, p2;

            p1 = polygon[0];
            for (i = 1; i <= N; i++)
            {
                p2 = polygon[i % N];
                if (p.Y > System.Math.Min(p1.Y, p2.Y))
                {
                    if (p.Y <= System.Math.Max(p1.Y, p2.Y))
                    {
                        if (p.X <= System.Math.Max(p1.X, p2.X))
                        {
                            if (p1.Y != p2.Y)
                            {
                                xinters = (p.Y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y) + p1.X;
                                if (p1.X == p2.X || p.X <= xinters)
                                    counter++;
                            }
                        }
                    }
                }
                p1 = p2;
            }

            if (counter % 2 == 0)
                return false;
            else
                return true;
        }

        public Hex pointToHex(Point pnt, Boolean cm)
        {
            Hex auxHex = new Hex(0, 0, 0, 0,HexOrientation.Pointy);
            for (int i = 0; i < ncolumnas; i++)
            {
                for (int j = 0; j < nfilas; j++)
                {
                    if (!InsidePolygon(HexesPix[i][j].Points, 6, pnt)) continue;
                    auxHex = HexesPix[i][j];
                   // if (!cm) { auxHex = HexesPix[i][j]; }
                   // else { auxHex = HexesCm[i][j]; }
                }
            }
            return auxHex;
        }

        public Image PaintAlpha(Hex Hex)
        {
            Bitmap aux = (Bitmap)inImage.Clone();
            Color c = new Color();

            int H = Nj_anchuraPix / 2; // centro 
            int B = Ni_alturaPix / 2; // del
            int D = (int)(distanciaCm * escala); //ojo

            int Xx = (int)Hex.X;
            int Yx = (int)Hex.Y;

            for (int ix = 0; ix < Nj_anchuraPix; ix++)
            {
                for (int iy = 0; iy < Ni_alturaPix; iy++)
                {
                    double auxX = Xx - H;
                    double auxY = Yx - B;
                    double auxx = ix - H;
                    double auxy = iy - B;
                    double prod_esc = auxX * auxx + auxY * auxy + D * D;
                    double mod_u = Math.Sqrt(auxX * auxX + auxY * auxY + D * D);
                    double mod_v = Math.Sqrt(auxx * auxx + auxy * auxy + D * D);
                    double cosang = prod_esc / (mod_u * mod_v);
                    if (cosang > 1) cosang = 1;
                    if (cosang < 0) cosang = 0;
                    double ang = Math.Acos(cosang);
                    double peso = Math.Exp(-2.77 * Math.Pow(ang / angAceptacion, 2));
                    int pAlpha = 255 - (int)(peso * 255);
                    if (ang >= 1.5 * angAceptacion) { c = Color.Gray; } else { c = Color.FromArgb(pAlpha, 128, 128, 128); }
                    aux.SetPixel(ix, iy, c);
                }
            }
            aux.SetPixel(Xx, Yx, Color.Red);
            return aux;
        }

        public string PrintCenters()
        {
            string aux = "";
            for (int i = 0; i < ncolumnas; i++)
            {
                for (int j = 0; j < nfilas; j++)
                {
                    aux = aux + string.Format("{0} \t {1} \t {2} \t {3} \t {4}\n", i, j, HexesPix[i][j].X, HexesPix[i][j].Y, HexesPix[i][j].AfectRect.Location.ToString());
                }
            }

            return aux;
        }
        
    }
}
