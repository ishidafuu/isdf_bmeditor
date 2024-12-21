<template>
  <div style="z-index: 0">
    <div :style="styleData" class="character-body"></div>
  </div>
</template>

<script lang="ts">
import { defineComponent, computed, ref, onMounted } from "vue";
import { useImageUtils } from "../../mixins/imageUtils.ts";

export default defineComponent({
  name: "BodyView",
  props: {
    bodyX: Number,
    bodyY: Number,
    activeCellIndex: Number,
    flipX: Number,
    flipY: Number,
    rotation: Number
  },
  setup(props) {
    const imagePath = '/nm2body.png';
    const transparentImagePath = ref<string | null>(null);
    const gridSize = 40;
    const { makeColorTransparent } = useImageUtils();

    const backgroundStyle = computed(() => {
      const col = Math.floor(props.activeCellIndex / gridSize);
      const row = props.activeCellIndex % gridSize;
      const bgPosX = -col * gridSize;
      const bgPosY = -row * gridSize;
      return {
        backgroundImage: `url(${transparentImagePath.value || imagePath})`,
        backgroundPosition: `${bgPosX}px ${bgPosY}px`
      };
    });

    const styleData = computed(() => {
      const scale = 4;
      const scaleX = props.flipX ? -1 : 1;
      const scaleY = props.flipY ? -1 : 1;
      const halfSize = 40 / 2;
      let translateX = props.bodyX * scale;
      let translateY = -props.bodyY * scale;
      const rotationAngle = props.rotation * 90;  // convert enum to degrees

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
        transform: `scale(${scaleX}, ${scaleY}) translate(${translateX}px, ${translateY}px) rotate(${rotationAngle}deg) scale(${scale})`
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
.character-body {
  left: 50%;
  top: 50%;
  width: 40px;
  height: 40px;
  background-repeat: no-repeat;
  transform-origin: center;
  image-rendering: pixelated;
  position: absolute;//border: 1px solid red;
}
</style>
