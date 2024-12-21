import { ref } from "vue";

export function useImageUtils() {
    const makeColorTransparent = (image: string, color: [number, number, number], callback: (arg0: string) => void) => {
        const canvas = document.createElement('canvas');
        const context = canvas.getContext('2d') as CanvasRenderingContext2D;

        const img = new Image();
        img.onload = () => {
            canvas.width = img.width;
            canvas.height = img.height;

            context.drawImage(img, 0, 0, img.width, img.height);

            const imageData = context.getImageData(0, 0, img.width, img.height);
            const data = imageData.data;

            for (let i = 0; i < data.length; i += 4) {
                if (data[i] === color[0] && data[i + 1] === color[1] && data[i + 2] === color[2]) {
                    // Make the pixel transparent
                    data[i + 3] = 0;
                }
            }

            context.putImageData(imageData, 0, 0);

            callback(canvas.toDataURL());
        };

        img.src = image;
    };

    return {
        makeColorTransparent,
    };
}
