using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH.Engine.Display {

    /// <summary>
    /// Set of constants that represent DOS ASCII codepage characters.
    /// </summary>
    public class CP437 {

        private static int ASCII_CODEPAGE = 437;
        private static Encoding ASCII_ENCODING = Encoding.GetEncoding(ASCII_CODEPAGE);


        public static char HORIZONTAL_BORDER =  ASCII_ENCODING.GetChars(new byte[] { 205 })[0];
        public static char VERTICAL_BORDER =    ASCII_ENCODING.GetChars(new byte[] { 186 })[0];
        public static char UPPER_LEFT_CORNER =  ASCII_ENCODING.GetChars(new byte[] { 201 })[0];
        public static char UPPER_RIGHT_CORNER = ASCII_ENCODING.GetChars(new byte[] { 187 })[0];
        public static char LOWER_LEFT_CORNER =  ASCII_ENCODING.GetChars(new byte[] { 200 })[0];
        public static char LOWER_RIGHT_CORNER = ASCII_ENCODING.GetChars(new byte[] { 188 })[0];
        public static char LEFT_FRAME_LINK =    ASCII_ENCODING.GetChars(new byte[] { 185 })[0];
        public static char RIGHT_FRAME_LINK =   ASCII_ENCODING.GetChars(new byte[] { 204 })[0];
        public static char TOP_FRAME_LINK =     ASCII_ENCODING.GetChars(new byte[] { 203 })[0];
        public static char BOTTOM_FRAME_LINK =  ASCII_ENCODING.GetChars(new byte[] { 202 })[0];
        public static char SOLID_BRICK =        ASCII_ENCODING.GetChars(new byte[] { 176 })[0];
 
    }
}
