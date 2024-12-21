<template>
  <div>
    <button @click="changeOffsetX(-1)">Move Left</button>
    <span>OffsetX (A,D): {{ offsetX }}</span>
    <button @click="changeOffsetX(1)">Move Right</button>
  </div>
  <div>
    <button @click="changeOffsetY(-1)">Move Down</button>
    <span>OffsetY (W,S): {{ offsetY }}</span>
    <button @click="changeOffsetY(1)">Move Up</button>
  </div>
</template>

<script lang="ts">
import {defineComponent, computed, onMounted, onBeforeUnmount} from 'vue'
import {useStore} from 'vuex'

export default defineComponent({
  props: {
    storeName: {
      type: String,
      required: true
    },
  },
  setup(props) {
    const store = useStore()

    const offsetX = computed(() => store.getters[`${props.storeName}/currentKomaState`].offsetX)
    const offsetY = computed(() => store.getters[`${props.storeName}/currentKomaState`].offsetY)

    const changeOffsetX = (amount: number) => {
      store.commit(`${props.storeName}/changeOffsetX`, amount)
    }

    const changeOffsetY = (amount: number) => {
      store.commit(`${props.storeName}/changeOffsetY`, amount)
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.ctrlKey) {
        return;
      }

      if (event.code === 'KeyA') {
        changeOffsetX(-1);
      } else if (event.code === 'KeyD') {
        changeOffsetX(1);
      } else if (event.code === 'KeyW') {
        changeOffsetY(1);
      } else if (event.code === 'KeyS') {
        changeOffsetY(-1);
      }
    }

    onMounted(() => {
      window.addEventListener('keydown', handleKeyDown);
    });

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeyDown);
    });

    return {
      offsetX,
      offsetY,
      changeOffsetX,
      changeOffsetY,
    }
  }
})
</script>