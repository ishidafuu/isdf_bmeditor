<template>
  <div :class="{ highlight: editMode === 'Item' }">
    <div>
      <button @click="changeItemX(-1)">Move Left</button>
      <span>Item X (A,D): {{ itemX }}</span>
      <button @click="changeItemX(1)">Move Right</button>
    </div>
    <div>
      <button @click="changeItemY(-1)">Move Down</button>
      <span>Item Y (W,S): {{ itemY }}</span>
      <button @click="changeItemY(1)">Move Up</button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, computed, onMounted, onBeforeUnmount } from 'vue'
import { useStore } from 'vuex'

export default defineComponent({
  setup() {
    const store = useStore()

    const itemX = computed(() => store.getters['charaCell/currentBodyState'].itemX)
    const itemY = computed(() => store.getters['charaCell/currentBodyState'].itemY)
    const editMode = computed(() => store.state.charaCell.editMode)

    const changeItemX = (amount: number) => {
      store.commit('charaCell/changeItemX', amount)
    }

    const changeItemY = (amount: number) => {
      store.commit('charaCell/changeItemY', amount)
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.ctrlKey) {
        return;
      }

      if (editMode.value !== 'Item') {
        return;
      }

      if (event.code === 'KeyA') {
        changeItemX(-1);
      } else if (event.code === 'KeyD') {
        changeItemX(1);
      } else if (event.code === 'KeyW') {
        changeItemY(1);
      } else if (event.code === 'KeyS') {
        changeItemY(-1);
      }
    }

    onMounted(() => {
      window.addEventListener('keydown', handleKeyDown);
    });

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeyDown);
    });

    return {
      itemX,
      itemY,
      changeItemX,
      changeItemY,
      editMode
    }
  }
})
</script>

<style scoped>
.highlight {
  color: yellow;
}
</style>
