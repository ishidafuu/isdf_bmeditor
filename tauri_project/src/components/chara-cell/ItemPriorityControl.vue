<template>
  <div :class="{ highlight: editMode === 'Item' }">
    <button @click="changePriority('up')">UP</button>
    <span>Item Z (F): {{ itemZ }}</span>
    <button @click="changePriority('down')">DOWN</button>
  </div>
</template>

<script lang="ts">
import { defineComponent, computed, onMounted, onBeforeUnmount } from 'vue'
import { useStore } from 'vuex'

export default defineComponent({
  setup() {
    const store = useStore()

    const itemZ = computed(() => store.getters['charaCell/currentBodyState'].itemZ)
    const editMode = computed(() => store.state.charaCell.editMode)

    const changePriority = (direction: string) => {
      if ((direction === 'up' && itemZ.value < 0) || (direction === 'down' && itemZ.value >= 0)) {
        store.commit('charaCell/toggleItemZ')
      }
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      if (editMode.value !== 'Item') {
        return;
      }

      if (event.code === 'KeyF') {
        changePriority(itemZ.value >= 0 ? 'down' : 'up');
      }
    }

    onMounted(() => {
      window.addEventListener('keydown', handleKeyDown);
    });

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeyDown);
    });

    return {
      itemZ,
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
