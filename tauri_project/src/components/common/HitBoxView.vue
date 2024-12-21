<template>
  <div :style="hitboxStyle" class="hitbox"></div>
</template>

<script lang="ts">
import {defineComponent, computed} from "vue";

export default defineComponent({
  props: {
    hitX: Number,
    hitY: Number,
    hitW: Number,
    hitH: Number,
  },
  setup(props) {
    const hitboxStyle = computed(() => {
      const scale = 4;
      const offset = 160;
      const x = (props.hitX - props.hitW) * scale + offset;
      const y = (-props.hitY - props.hitH) * scale + offset;
      const width = props.hitW * 2 * scale;
      const height = props.hitH * 2 * scale;

      return {
        left: `${x}px`,
        top: `${y}px`,
        width: `${width}px`,
        height: `${height}px`,
      };
    });

    return {
      hitboxStyle
    };
  }
});
</script>

<style scoped>
.hitbox {
  position: absolute;
  border: 1px solid yellow;
  background-color: rgba(255, 255, 0, 0.4);
}
</style>
