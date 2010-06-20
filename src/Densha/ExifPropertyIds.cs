using System;
using System.Collections.Generic;
using System.Text;

namespace Densha {
    enum ExifPropertyIds : int {
        DateTimeOriginal = 0x9003,
        SubSecTimeOriginal = 0x9291,
    }

    enum ExifPropertyLength : int {
        DateTimeOriginal = 20,
        SubSecTimeOriginal = -1,
    }
}
