<template>
  <div :class="{ highlight: editMode === 'Face' }">
    <button @click="changePriority('up')">UP</button>
    <span>Face Z (F): {{ faceZ }}</span>
    <button @click="changePriority('down')">DOWN</button>
  </div>
</template>

<script lang="ts">
import { defineComponent, computed, onMounted, onBeforeUnmount } from 'vue'
import { useStore } from 'vuex'

export default defineComponent({
  setup() {
    const store = useStore()

    const faceZ = computed(() => store.getters['charaCell/currentBodyState'].faceZ)
    const editMode = computed(() => store.state.charaCell.editMode)

    const changePriority = (direction: string) => {
      if ((direction === 'up' && faceZ.value < 0) || (direction === 'down' && faceZ.value >= 0)) {
        store.commit('charaCell/toggleFacePriority')
      }
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      if (editMode.value !== 'Face') {
        return;
      }

      if (event.code === 'KeyF') {
        changePriority(faceZ.value >= 0 ? 'down' : 'up');
      }
    }

    onMounted(() => {
      window.addEventListener('keydown', handleKeyDown);
    });

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeyDown);
    });

    return {
      faceZ,
      changePriority,
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
