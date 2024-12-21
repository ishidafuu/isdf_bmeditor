<template>
  <div class="hitbox-control" @mousedown="handleMouseDown" @mousemove="handleMouseMove" @mouseup="handleMouseUp">
  </div>
</template>

<script lang="ts">
import {defineComponent, ref} from "vue";
import {useStore} from 'vuex';

export default defineComponent({
  name: "HitBoxControl",
  props: {
    storeName: {
      type: String,
      required: true
    }
  },
  setup(props) {
    const offsetX = -216;
    const offsetY = -292;
    const scale = 4;
    const store = useStore();
    const isDragging = ref(false);
    const initialX = ref(0);
    const initialY = ref(0);

    const handleMouseDown = (e: MouseEvent) => {
      isDragging.value = true;
      initialX.value = e.clientX + offsetX;
      initialY.value = e.clientY + offsetY;

      const hitX = Math.floor(initialX.value / scale);
      const hitY = Math.floor(-initialY.value / scale);

      store.commit(`${props.storeName}/setHitX`, hitX);
      store.commit(`${props.storeName}/setHitY`, hitY);
      store.commit(`${props.storeName}/setHitW`, 0);
      store.commit(`${props.storeName}/setHitH`, 0);
    };

    const handleMouseMove = (e: MouseEvent) => {
      if (!isDragging.value) return;

      const hitW = Math.floor(Math.abs(e.clientX + offsetX - initialX.value) / scale);
      const hitH = Math.floor(Math.abs(e.clientY + offsetY - initialY.value) / scale);

      // Storeにデータを保存
      store.commit(`${props.storeName}/setHitW`, hitW);
      store.commit(`${props.storeName}/setHitH`, hitH);
    };

    const handleMouseUp = () => {
      isDragging.value = false;
    };

    return {
      handleMouseDown,
      handleMouseMove,
      handleMouseUp,
    };
  },
});
</script>

<style scoped>
.hitbox-control {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  border: 1px solid yellow;
}

.hitbox-rect {
  position: absolute;
  border: 2px solid red;
}
</style>