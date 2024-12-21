<template>
  <div>
    <div>
      <button @click="changeSeNo(-1)">Decrease SeNo</button>
      <span>SeNo (J,L): {{ seNo }}</span>
      <button @click="changeSeNo(1)">Increase SeNo</button>
    </div>
    <div>
      <span>Is Loop Se: {{ isLoopSe }}</span>
      <button @click="toggleLoopSe">Toggle Loop Se</button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, computed, onMounted, onBeforeUnmount } from 'vue'
import { useStore } from 'vuex'

export default defineComponent({
  props: {
    storeName: {
      type: String,
      required: true
    },
  },
  setup(props) {
    const store = useStore()

    const seNo = computed(() => store.getters[`${props.storeName}/currentKomaState`].seNo)
    const isLoopSe = computed(() => store.getters[`${props.storeName}/currentKomaState`].isLoopSe)

    const changeSeNo = (amount: number) => {
      store.commit(`${props.storeName}/changeSeNo`, amount)
    }

    const toggleLoopSe = () => {
      store.commit(`${props.storeName}/toggleLoopSe`)
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.ctrlKey) {
        return;
      }

      if (event.code === 'KeyJ') {
        changeSeNo(-1);
      } else if (event.code === 'KeyL') {
        changeSeNo(1);
      }
    }

    onMounted(() => {
      window.addEventListener('keydown', handleKeyDown);
    })

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeyDown);
    })

    return {
      seNo,
      isLoopSe,
      changeSeNo,
      toggleLoopSe,
    }
  }
})
</script>
