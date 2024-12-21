<template>
  <div :style="styleData" class="character-item"></div>
</template>

<script lang="ts">
import {defineComponent, computed, ref, onMounted} from "vue";
import {useImageUtils} from "../../mixins/imageUtils.ts";

export default defineComponent({
  name: "ItemView",
  props: {
    itemAngle: Number,
    itemZ: Number,
    itemX: Number,
    itemY: Number,
    flipX: Number,
    flipY: Number,
    rotation: Number
  },
  setup(props) {
    const imagePath = '/item.png';
    const transparentImagePath = ref<string | null>(null);
    const gridSize = 32;
    const {makeColorTransparent} = useImageUtils();

    const backgroundStyle = computed(() => {
      const bgPosX = -props.itemAngle * gridSize;
      const bgPosY = 0;
      return {
        backgroundImage: `url(${transparentImagePath.value || imagePath})`,
        backgroundPosition: `${bgPosX}px ${bgPosY}px`
      };
    });

    const styleData = computed(() => {
      const scale = 4;
      const halfSize = 32 / 2;
      const scaleX = props.flipX ? -1 : 1;
      const scaleY = props.flipY ? -1 : 1;
      let translateX = props.itemX * scale;
      let translateY = -props.itemY * scale;
      const rotationAngle = props.rotation * 90;  // convert enum to degrees

      // Rotation-based coordinate transformation
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
        zIndex: props.itemZ
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
.character-item {
  left: 50%;
  top: 50%;
  position: absolute;
  width: 32px;
  height: 32px;
  background-repeat: no-repeat;
  transform-origin: center;
  image-rendering: pixelated;//border: 1px solid magenta;
}
</style>
