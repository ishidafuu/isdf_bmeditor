<template>
  <div>
    <button :class="{ highlight: editMode === 'Body' }" @click="changeEditMode('Body')">Body Edit Mode (1)</button>
    <button :class="{ highlight: editMode === 'Face' }" @click="changeEditMode('Face')">Face Edit Mode (2)</button>
    <button :class="{ highlight: editMode === 'Item' }" @click="changeEditMode('Item')">Item Edit Mode (3)</button>
  </div>
</template>

<script lang="ts">
import { defineComponent, computed, onMounted, onBeforeUnmount } from 'vue'
import { useStore } from 'vuex'

export default defineComponent({
  setup() {
    const store = useStore()
    const editMode = computed(() => store.state.charaCell.editMode)

    const changeEditMode = (mode: string) => {
      store.commit('charaCell/setEditMode', mode)
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.code === 'Digit1') {
        changeEditMode('Body');
      } else if (event.code === 'Digit2') {
        changeEditMode('Face');
      } else if (event.code === 'Digit3') {
        changeEditMode('Item');
      }
    }

    onMounted(() => {
      window.addEventListener('keydown', handleKeyDown);
    });

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeyDown);
    });

    return {
      editMode,
      changeEditMode
    }
  }
})
</script>

<style scoped>
.highlight {
  color: yellow;
}
</style>
