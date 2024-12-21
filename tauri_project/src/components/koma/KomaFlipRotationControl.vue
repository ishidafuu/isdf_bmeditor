<template>
  <div>
    <button @click="toggleFlipX">Toggle FlipX</button>
    <span>FlipX (F): {{ flipX }}</span>
    <button @click="toggleFlipY">Toggle FlipY</button>
    <span>FlipY (G): {{ flipY }}</span>
  </div>
  <div>
    <button @click="changeRotation(-1)">Rotate Counterclockwise</button>
    <span>Rotation (R): {{ rotation }}</span>
    <button @click="changeRotation(1)">Rotate Clockwise</button>
  </div>
</template>

<script lang="ts">
import { defineComponent, computed, onMounted, onBeforeUnmount } from 'vue';
import { useStore } from 'vuex';

export default defineComponent({
  props: {
    storeName: {
      type: String,
      required: true,
    },
  },
  setup(props) {
    const store = useStore();

    const flipX = computed(() => store.getters[`${props.storeName}/currentKomaState`].flipX);
    const flipY = computed(() => store.getters[`${props.storeName}/currentKomaState`].flipY);
    const rotation = computed(() => store.getters[`${props.storeName}/currentKomaState`].rotation);

    const toggleFlipX = () => {
      store.commit(`${props.storeName}/toggleFlipX`);
    };

    const toggleFlipY = () => {
      store.commit(`${props.storeName}/toggleFlipY`);
    };

    const changeRotation = (amount: number) => {
      store.commit(`${props.storeName}/changeRotation`, amount);
    };

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.ctrlKey) {
        return;
      }

      if (event.code === 'KeyF') {
        toggleFlipX();
      } else if (event.code === 'KeyG') {
        toggleFlipY();
      } else if (event.code === 'KeyR') {
        changeRotation(1);
      }
    };

    onMounted(() => {
      window.addEventListener('keydown', handleKeyDown);
    });

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeyDown);
    });

    return {
      flipX,
      flipY,
      rotation,
      toggleFlipX,
      toggleFlipY,
      changeRotation,
    };
  },
});
</script>