<template>
  <div>
    <button @click="changeFrame(-1)">Decrease Frame</button>
    <span>Frame (Q,E): {{ frame }}</span>
    <button @click="changeFrame(1)">Increase Frame</button>
    <button @click="changeFrameAll">Set All Frames</button>
  </div>
</template>

<script lang="ts">
import { defineComponent, computed, onMounted, onBeforeUnmount, toRefs } from 'vue'
import { useStore } from 'vuex'

export default defineComponent({
  props: {
    storeName: {
      type: String,
      required: true
    }
  },
  setup(props) {
    const store = useStore()
    const { storeName } = toRefs(props)

    const frame = computed(() => store.getters[`${storeName.value}/currentKomaState`].frame)

    const changeFrame = (amount: number) => {
      store.commit(`${storeName.value}/changeFrame`, amount)
    }

    const changeFrameAll = () => {
      store.commit(`${storeName.value}/changeFrameAll`)
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.ctrlKey) {
        return;
      }

      if (event.code === 'KeyQ') {
        changeFrame(-1);
      } else if (event.code === 'KeyE') {
        changeFrame(1);
      }
    }

    onMounted(() => {
      window.addEventListener('keydown', handleKeyDown);
    });

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeyDown);
    });

    return {
      frame,
      changeFrame,
      changeFrameAll,
    }
  }
})
</script>
