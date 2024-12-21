<template>
  <div>
    <button @click="changeCellIndex(-1)">-</button>
    <span>Cell Index (Cursor): {{ activeCellIndex }}</span>
    <button @click="changeCellIndex(1)">+</button>
  </div>
</template>

<script lang="ts">
import { defineComponent, computed, onMounted, onBeforeUnmount } from 'vue'
import { useStore } from 'vuex'

export default defineComponent({
  setup() {
    const store = useStore()

    const activeCellIndex = computed(() => store.state.charaCell.activeCellIndex)

    const changeCellIndex = (amount: number) => {
      store.commit('charaCell/changeCellIndex', amount)
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.shiftKey) {
        return;
      }

      if (event.code === 'ArrowLeft') {
        changeCellIndex(-1);
      } else if (event.code === 'ArrowRight') {
        changeCellIndex(1);
      } else if (event.code === 'ArrowUp') {
        changeCellIndex(10);
      } else if (event.code === 'ArrowDown') {
        changeCellIndex(-10);
      }
    }

    onMounted(() => {
      window.addEventListener('keydown', handleKeyDown);
    });

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeyDown);
    });

    return {
      activeCellIndex,
      changeCellIndex
    }
  }
})
</script>