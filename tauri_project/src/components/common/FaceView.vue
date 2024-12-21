<template>
  <div :style="styleData" class="character-face"></div>
</template>

<script lang="ts">
import {defineComponent, computed, ref, onMounted} from "vue";
import {useImageUtils} from "../../mixins/imageUtils.ts";

export default defineComponent({
  name: "FaceView",
  props: {
    faceIndex: Number,
    faceAngle: Number,
    faceX: Number,
    faceY: Number,
    faceZ: Number,
    flipX: Number,
    flipY: Number,
    rotation: Number
  },
  setup(props) {
    const imagePath = '/face.png';
    const transparentImagePath = ref<string | null>(null);
    const gridSize = 16;
    const {makeColorTransparent} = useImageUtils();

    const backgroundStyle = computed(() => {
      const col = props.faceIndex;
      const row = props.faceAngle;
      const bgPosX = -col * gridSize;
      const bgPosY = -row * gridSize;
      return {
        backgroundImage: `url(${transparentImagePath.value || imagePath})`,
        backgroundPosition: `${bgPosX}px ${bgPosY}px`
      };
    });

    const styleData = computed(() => {
      const scale = 4;
      const halfSize = 16 / 2;
      const scaleX = props.flipX ? -1 : 1;
      const scaleY = props.flipY ? -1 : 1;
      let translateX = props.faceX * scale;
      let translateY = -props.faceY * scale;
      const rotationAngle = props.rotation * 90;

      switch (props.rotation) {
        case 1:  // 90 degrees
          [translateX, translateY] = [-translateY, translateX];
          break;
        case 2:  // 180 degrees
          [translateX, translateY] = [-translateX, -translateY];
          break;
        case 3:  // 270 degrees
          [translateX, translateY] = [translateY, -translateX];
          break;
      }
      translateX -= halfSize * scaleX;
      translateY -= halfSize * scaleY;

      return {
        ...backgroundStyle.value,
        transform: `scale(${scaleX}, ${scaleY}) translate(${translateX}px, ${translateY}px) rotate(${rotationAngle}deg) scale(${scale})`,
        zIndex: props.faceZ
      };
    });

    onMounted(() => {
      makeColorTransparent(imagePath, [255, 0, 255], (transparentImg) => {
        transparentImagePath.value = transparentImg;
      });
    });

    return {
      styleData
    };
  },
});
</script>

<style scoped>
.character-face {
  left: 50%;
  top: 50%;
  position: absolute;
  width: 16px;
  height: 16px;
  background-repeat: no-repeat;
  transform-origin: center;
  image-rendering: pixelated; //border: 1px solid cyan;
}
</style>
